using AutoMapper;
using Microsoft.Extensions.Logging;
using SovosCase.Application.Interfaces;
using SovosCase.Application.Interfaces.Mongo;
using SovosCase.Application.Interfaces.Sql;
using SovosCase.Application.Models.Enums;
using SovosCase.Application.Models.Requests;
using SovosCase.Application.Models.Responses;
using SovosCase.Application.Queries.GetInvoicesToStoreFromRegister;
using SovosCase.Application.Responses;
using SovosCase.Domain.Entities.Mongo;
using SovosCase.Domain.Entities.Sql;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SovosCase.Application.Services
{
    public class InvoiceSqlService : IInvoiceSqlService
    {
        private readonly IInvoiceSqlRepository _invoiceSqlRepository;
        private readonly IInvoiceItemSqlRepository _invoiceItemSqlRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceSqlService> _logger;

        public InvoiceSqlService(IInvoiceSqlRepository invoiceSqlRepository, IInvoiceItemSqlRepository invoiceItemSqlRepository, IMapper mapper, ILogger<InvoiceSqlService> logger)
        {
            _invoiceSqlRepository = invoiceSqlRepository ?? throw new ArgumentNullException(nameof(invoiceSqlRepository));
            _invoiceItemSqlRepository = invoiceItemSqlRepository ?? throw new ArgumentNullException(nameof(invoiceItemSqlRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<BaseResponse<List<GetInvoiceSqlResponse>>> GetAllInvoices()
        {
            var invoices = await _invoiceSqlRepository.GetAllAsync();
            if (invoices == null)
            {
                _logger.LogError($"Failed to GetAllInvoices from Store.");
                return BaseResponse<List<GetInvoiceSqlResponse>>.Fail("Failed to GetAllInvoices from Store.", 404);
            }
            return BaseResponse<List<GetInvoiceSqlResponse>>.Success(_mapper.Map<List<GetInvoiceSqlResponse>>(invoices), 200);
        }

        public async Task<BaseResponse<GetInvoiceSqlResponse>> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceSqlRepository.GetByIdAsync(id, new List<string>() { "InvoiceItems" });
            if (invoice == null)
                return BaseResponse<GetInvoiceSqlResponse>.Fail($"No invoice found in Store. InvoiceId: '{id}'", 404);
            return BaseResponse<GetInvoiceSqlResponse>.Success(_mapper.Map<GetInvoiceSqlResponse>(invoice), 200);
        }

        public async Task<BaseResponse<GetInvoiceSqlResponse>> GetInvoiceByInvoiceId(string invoiceId)
        {
            var invoice = await _invoiceSqlRepository.GetFirstWhereAsync(i => i.InvoiceId == invoiceId, new List<string>() { "InvoiceItems" });
            if (invoice == null)
                return BaseResponse<GetInvoiceSqlResponse>.Fail($"No invoice found in Store. InvoiceId: '{invoiceId}'", 404);
            return BaseResponse<GetInvoiceSqlResponse>.Success(_mapper.Map<GetInvoiceSqlResponse>(invoice), 200);
        }

        public async Task<BaseResponse<List<GetInvoiceSqlResponse>>> GetInvoiceList(GetInvoiceListSqlRequest getInvoiceListRequest)
        {
            Expression<Func<InvoiceSql, bool>>? predicate = default;
            if (!string.IsNullOrWhiteSpace(getInvoiceListRequest.SearchWord))
            {
                _ = getInvoiceListRequest.SearchIn switch
                {
                    InvoiceSearchInType.InvoiceId => predicate = invoice => invoice.InvoiceId.Contains(getInvoiceListRequest.SearchWord),
                    InvoiceSearchInType.SenderTitle => predicate = invoice => invoice.SenderTitle.Contains(getInvoiceListRequest.SearchWord),
                    InvoiceSearchInType.ReceiverTitle => predicate = invoice => invoice.ReceiverTitle.Contains(getInvoiceListRequest.SearchWord),
                    InvoiceSearchInType.Date => predicate = invoice => invoice.Date.Contains(getInvoiceListRequest.SearchWord),
                    _ => predicate = invoice => invoice.InvoiceId.Contains(getInvoiceListRequest.SearchWord)
                };
            }

            Func<IQueryable<InvoiceSql>, IOrderedQueryable<InvoiceSql>>? orderBy = default;
            if (getInvoiceListRequest.OrderBy != null || getInvoiceListRequest.IsReversed)
            {
                _ = getInvoiceListRequest.OrderBy switch
                {
                    InvoiceOrderByType.CreatedOn => orderBy = getInvoiceListRequest.IsReversed ? query => query.OrderByDescending(invoice => invoice.CreatedOn) : query => query.OrderBy(invoice => invoice.CreatedOn),
                    InvoiceOrderByType.ModifiedOn => orderBy = getInvoiceListRequest.IsReversed ? query => query.OrderByDescending(invoice => invoice.ModifiedOn) : query => query.OrderBy(invoice => invoice.ModifiedOn),
                    InvoiceOrderByType.InvoiceId => orderBy = getInvoiceListRequest.IsReversed ? query => query.OrderByDescending(invoice => invoice.InvoiceId) : query => query.OrderBy(invoice => invoice.InvoiceId),
                    InvoiceOrderByType.Date => orderBy = getInvoiceListRequest.IsReversed ? query => query.OrderByDescending(invoice => invoice.Date) : query => query.OrderBy(invoice => invoice.Date),
                    _ => orderBy = getInvoiceListRequest.IsReversed ? query => query.OrderByDescending(invoice => invoice.CreatedOn) : query => query.OrderBy(invoice => invoice.CreatedOn)
                };
            }

            var invoices = await _invoiceSqlRepository.GetAsync(getInvoiceListRequest.PageNumber, getInvoiceListRequest.PageSize, orderBy, predicate, getInvoiceListRequest.Includes ?? new List<string>() { "InvoiceItems" });

            if (invoices == null)
            {
                _logger.LogError($"Failed to GetInvoiceList from Store.");
                return BaseResponse<List<GetInvoiceSqlResponse>>.Fail("Failed to GetInvoiceList from Store.", 500);
            }

            int? count = default;
            if (getInvoiceListRequest.NeedCount)
                count = await _invoiceSqlRepository.CountAsync(predicate);

            var sorting = new SortedListResponse() { PageNumber = getInvoiceListRequest.PageNumber, PageSize = getInvoiceListRequest.PageSize, IsReversed = getInvoiceListRequest.IsReversed, NeedCount = getInvoiceListRequest.NeedCount, Count = count, SearchWord = getInvoiceListRequest.SearchWord };

            return BaseResponse<List<GetInvoiceSqlResponse>>.Success(_mapper.Map<List<GetInvoiceSqlResponse>>(invoices), 200, sorting);
        }

        public async Task<bool> StoreInvoice(GetInvoicesToStoreFromRegisterQueryResponse invoiceMongo)
        {
            var invoiceAdded = await _invoiceSqlRepository.AddAsync(_mapper.Map<InvoiceSql>(invoiceMongo.InvoiceHeader));

            if (invoiceAdded == null)
            {
                _logger.LogError($"Failed to StoreInvoices.");
                return false;
            }

            try
            {
                var invoiceLineToAdd = _mapper.Map<List<InvoiceItemSql>>(invoiceMongo.InvoiceLine);
                invoiceLineToAdd.ForEach(il => { il.InvoiceId = invoiceAdded.InvoiceId; il.InvoiceGuid = invoiceAdded.Id; });

                var invoiceLineAdded = await _invoiceItemSqlRepository.AddRangeAsync(invoiceLineToAdd);

                if (invoiceLineAdded < invoiceMongo.InvoiceLine.Count)
                    throw new Exception("Updated count does not match the ItemCount.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to StoreInvoiceItems. ErrorMessage: {ex.ToString()}");
                await _invoiceItemSqlRepository.DeleteInvoiceItemsByInvoiceId(invoiceAdded.InvoiceId, invoiceAdded.Id);
                await _invoiceSqlRepository.DeleteAsync(invoiceAdded.Id); // Cascading ??
                return false;
            }

            return true;
        }
    }
}

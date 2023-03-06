using SovosCase.Application.Models.Requests;
using SovosCase.Application.Models.Responses;
using SovosCase.Application.Queries.GetInvoicesToStoreFromRegister;
using SovosCase.Application.Responses;

namespace SovosCase.Application.Interfaces
{
    public interface IInvoiceSqlService
    {
        Task<BaseResponse<List<GetInvoiceSqlResponse>>> GetAllInvoices();
        Task<BaseResponse<GetInvoiceSqlResponse>> GetInvoiceById(Guid id);
        Task<BaseResponse<GetInvoiceSqlResponse>> GetInvoiceByInvoiceId(string invoiceId);
        Task<BaseResponse<List<GetInvoiceSqlResponse>>> GetInvoiceList(GetInvoiceListSqlRequest getInvoiceListRequest);
        Task<bool> StoreInvoice(GetInvoicesToStoreFromRegisterQueryResponse invoiceMongo);
    }
}

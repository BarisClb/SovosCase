using AutoMapper;
using SovosCase.Application.Models.Responses;
using SovosCase.Domain.Entities.Mongo;
using SovosCase.Domain.Entities.Sql;

namespace SovosCase.Infrastructure.AutoMapper
{
    public class InvoiceSqlProfile : Profile
    {
        public InvoiceSqlProfile()
        {
            CreateMap<InvoiceSql, GetInvoiceSqlResponse>().ReverseMap();
            CreateMap<InvoiceItemSql, GetInvoiceItemSqlResponse>().ReverseMap();
        }
    }
}

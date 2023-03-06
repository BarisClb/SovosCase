using AutoMapper;
using SovosCase.Application.Queries.GetInvoicesToStoreFromRegister;
using SovosCase.Domain.Entities.Mongo;
using SovosCase.Domain.Entities.Sql;

namespace SovosCase.Infrastructure.AutoMapper
{
    public class InvoiceCrossProfile : Profile
    {
        public InvoiceCrossProfile()
        {
            CreateMap<InvoiceItemSql, InvoiceLineMongo>().ForMember(dest => dest.Id, opt => opt.MapFrom(from => from.InvoiceItemId));
            CreateMap<InvoiceLineMongo, InvoiceItemSql>().ForMember(dest => dest.InvoiceItemId, opt => opt.MapFrom(from => from.Id))
                                                         .ForMember(dest => dest.Id, opt => opt.Ignore())
                                                         .ForMember(dest => dest.InvoiceGuid, opt => opt.Ignore())
                                                         .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
                                                         .ForMember(dest => dest.Invoice, opt => opt.Ignore())
                                                         .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                                                         .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore());
            CreateMap<InvoiceSql, InvoiceHeaderMongo>().ReverseMap();
            CreateMap<InvoiceItemSql, GetInvoicesToStoreFromRegisterQueryResponse>().ForMember(dest => dest.InvoiceLine, opt => opt.MapFrom(src => src))
                                                                                    .ForMember(dest => dest.InvoiceHeader, opt => opt.Ignore());
        }
    }
}
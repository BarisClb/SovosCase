using AutoMapper;
using SovosCase.Application.Commands.CreateInvoiceRegister;
using SovosCase.Application.Commands.UpdateInvoiceRegister;
using SovosCase.Application.Queries.GetInvoiceByIdFromRegister;
using SovosCase.Application.Queries.GetInvoiceHeadersFromRegister;
using SovosCase.Application.Queries.GetInvoicesToStoreFromRegister;
using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Infrastructure.AutoMapper
{
    public class InvoiceMongoProfile : Profile
    {
        public InvoiceMongoProfile()
        {
            CreateMap<InvoiceMongo, CreateInvoiceRegisterCommandRequest>().ForMember(invoice => invoice.InvoiceHeader, options => options.MapFrom(invoice => invoice.InvoiceHeader))
                                                                          .ForMember(invoice => invoice.InvoiceLine, options => options.MapFrom(invoice => invoice.InvoiceLine))
                                                                          .ReverseMap();
            CreateMap<CreateInvoiceRegisterCommandRequest, CreateInvoiceRegisterCommandResponse>().ForMember(invoice => invoice.InvoiceHeader, options => options.MapFrom(invoice => invoice.InvoiceHeader))
                                                                                                  .ForMember(invoice => invoice.InvoiceLine, options => options.MapFrom(invoice => invoice.InvoiceLine))
                                                                                                  .ReverseMap();

            CreateMap<InvoiceMongo, UpdateInvoiceRegisterCommandResponse>().ReverseMap();

            CreateMap<InvoiceHeaderMongo, GetInvoiceHeadersFromRegisterQueryResponse>().ReverseMap();
            CreateMap<InvoiceMongo, GetInvoiceHeadersFromRegisterQueryResponse>().ConstructUsing((source, context) => context.Mapper.Map<GetInvoiceHeadersFromRegisterQueryResponse>(source.InvoiceHeader))
                                                                                 .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.InvoiceId))
                                                                                 .ForMember(dest => dest.SenderTitle, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.SenderTitle))
                                                                                 .ForMember(dest => dest.SenderTitle, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.SenderTitle))
                                                                                 .ForMember(dest => dest.ReceiverTitle, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.ReceiverTitle))
                                                                                 .ForMember(dest => dest.Date, opt => opt.MapFrom(invoice => invoice.InvoiceHeader.Date))
                                                                                 .ReverseMap();

            CreateMap<InvoiceMongo, GetInvoiceByIdFromRegisterQueryResponse>().ReverseMap();
            CreateMap<InvoiceMongo, GetInvoicesToStoreFromRegisterQueryResponse>().ForMember(invoice => invoice.InvoiceHeader, options => options.MapFrom(invoice => invoice.InvoiceHeader))
                                                                                  .ForMember(invoice => invoice.InvoiceLine, options => options.MapFrom(invoice => invoice.InvoiceLine))
                                                                                  .ReverseMap();
        }
    }
}

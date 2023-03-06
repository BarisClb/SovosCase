using MediatR;
using SovosCase.Application.Responses;
using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Application.Commands.CreateInvoiceRegister
{
    public class CreateInvoiceRegisterCommandRequest : IRequest<BaseResponse<CreateInvoiceRegisterCommandResponse>>
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public ICollection<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}

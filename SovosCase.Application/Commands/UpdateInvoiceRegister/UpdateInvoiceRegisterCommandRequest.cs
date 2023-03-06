using MediatR;
using SovosCase.Application.Responses;

namespace SovosCase.Application.Commands.UpdateInvoiceRegister
{
    public class UpdateInvoiceRegisterCommandRequest : IRequest<BaseResponse<UpdateInvoiceRegisterCommandResponse>>
    {
        public string InvoiceId { get; set; }
        public string? SenderTitle { get; set; }
        public string? ReceiverTitle { get; set; }
        public string? Date { get; set; }
        public bool? IsStored { get; set; }
    }
}

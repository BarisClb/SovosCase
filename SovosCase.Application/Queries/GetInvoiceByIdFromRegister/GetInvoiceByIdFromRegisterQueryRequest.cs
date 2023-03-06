using MediatR;
using SovosCase.Application.Responses;

namespace SovosCase.Application.Queries.GetInvoiceByIdFromRegister
{
    public class GetInvoiceByIdFromRegisterQueryRequest : IRequest<BaseResponse<GetInvoiceByIdFromRegisterQueryResponse>>
    {
        public string InvoiceId { get; set; }
    }
}

using MediatR;
using SovosCase.Application.Responses;

namespace SovosCase.Application.Queries.GetInvoiceHeadersFromRegister
{
    public class GetInvoiceHeadersFromRegisterQueryRequest : IRequest<BaseResponse<List<GetInvoiceHeadersFromRegisterQueryResponse>>>
    { }
}

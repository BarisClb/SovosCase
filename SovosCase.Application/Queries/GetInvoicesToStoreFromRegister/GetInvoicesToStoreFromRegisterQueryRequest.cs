using MediatR;

namespace SovosCase.Application.Queries.GetInvoicesToStoreFromRegister
{
    public class GetInvoicesToStoreFromRegisterQueryRequest : IRequest<List<GetInvoicesToStoreFromRegisterQueryResponse>>
    { }
}

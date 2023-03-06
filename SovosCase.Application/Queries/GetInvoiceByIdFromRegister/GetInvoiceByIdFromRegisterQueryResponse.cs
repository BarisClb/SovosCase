using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Application.Queries.GetInvoiceByIdFromRegister
{
    public class GetInvoiceByIdFromRegisterQueryResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public IList<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}

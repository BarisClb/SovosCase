using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Application.Queries.GetInvoicesToStoreFromRegister
{
    public class GetInvoicesToStoreFromRegisterQueryResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public List<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}

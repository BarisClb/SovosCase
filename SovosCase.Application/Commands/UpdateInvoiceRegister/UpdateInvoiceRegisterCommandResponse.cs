using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Application.Commands.UpdateInvoiceRegister
{
    public class UpdateInvoiceRegisterCommandResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public IEnumerable<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}

using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Application.Commands.CreateInvoiceRegister
{
    public class CreateInvoiceRegisterCommandResponse
    {
        public InvoiceHeaderMongo InvoiceHeader { get; set; }
        public ICollection<InvoiceLineMongo> InvoiceLine { get; set; }
    }
}

using MongoDB.Driver;
using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Application.Interfaces.Mongo
{
    public interface IInvoiceMongoRepository : IBaseMongoRepository<InvoiceMongo>
    {
        Task<InvoiceMongo> GetByIdAsync(string id);
        Task<InvoiceMongo> UpdateAndGetByIdAsync(string invoiceId, UpdateDefinition<InvoiceMongo> updateDefination);
    }
}

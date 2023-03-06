using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SovosCase.Application.Interfaces.Mongo;
using SovosCase.Application.Settings;
using SovosCase.Domain.Entities.Mongo;

namespace SovosCase.Persistence.Repositories.Mongo
{
    public class InvoiceMongoRepository : BaseMongoRepository<InvoiceMongo>, IInvoiceMongoRepository
    {
        public InvoiceMongoRepository(IOptions<MongoDbSettings> databaseSettings) : base(databaseSettings.Value.ConnectionString, databaseSettings.Value.DatabaseName, databaseSettings.Value.InvoiceCollectionName)
        { }


        public async Task<InvoiceMongo> GetByIdAsync(string id)
        {
            return await (await _collection.FindAsync(x => x.InvoiceHeader.InvoiceId.Equals(id))).FirstOrDefaultAsync();
        }

        public async Task<InvoiceMongo> UpdateAndGetByIdAsync(string invoiceId, UpdateDefinition<InvoiceMongo> updateDefination)
        {
            return await _collection.FindOneAndUpdateAsync<InvoiceMongo>(x => x.InvoiceHeader.InvoiceId.Equals(invoiceId), updateDefination, new FindOneAndUpdateOptions<InvoiceMongo> { ReturnDocument = ReturnDocument.After });
        }
    }
}

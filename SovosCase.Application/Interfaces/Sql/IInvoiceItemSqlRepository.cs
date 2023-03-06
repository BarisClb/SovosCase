using SovosCase.Domain.Entities.Sql;

namespace SovosCase.Application.Interfaces.Sql
{
    public interface IInvoiceItemSqlRepository : IBaseSqlRepository<InvoiceItemSql>
    {
        Task<int> DeleteInvoiceItemsByInvoiceId(string? invoiceId = default, Guid? invoiceGuid = default);
    }
}

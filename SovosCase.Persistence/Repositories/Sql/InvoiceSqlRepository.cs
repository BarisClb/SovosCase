using SovosCase.Application.Interfaces.Sql;
using SovosCase.Domain.Entities.Sql;
using SovosCase.Persistence.Contexts;

namespace SovosCase.Persistence.Repositories.Sql
{
    public class InvoiceSqlRepository : BaseSqlRepository<InvoiceSql>, IInvoiceSqlRepository
    {
        public InvoiceSqlRepository(SovosCaseDbContext context) : base(context)
        { }
    }
}

using Microsoft.EntityFrameworkCore;
using SovosCase.Domain.Entities.Sql;
using SovosCase.Domain.Settings;
using System.Reflection;

namespace SovosCase.Persistence.Contexts
{
    public class SovosCaseDbContext : DbContext
    {
        public SovosCaseDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<InvoiceSql> Invoices { get; set; }
        public DbSet<InvoiceItemSql> InvoiceItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(InvoiceItemSqlSettings)));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntitySql>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                    data.Entity.CreatedOn = DateTime.UtcNow;
                else if (data.State == EntityState.Modified)
                    data.Entity.ModifiedOn = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

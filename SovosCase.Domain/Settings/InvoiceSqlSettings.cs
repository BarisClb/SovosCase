using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SovosCase.Domain.Entities.Sql;

namespace SovosCase.Domain.Settings
{
    public class InvoiceSqlSettings : IEntityTypeConfiguration<InvoiceSql>
    {
        public void Configure(EntityTypeBuilder<InvoiceSql> builder)
        {
            builder.ToTable("Invoices").HasKey(x => x.Id);

            builder.HasIndex(i => i.InvoiceId).IsUnique();
            builder.Property(i => i.SenderTitle).IsRequired();
            builder.Property(i => i.ReceiverTitle).IsRequired();
            builder.Property(i => i.Date).IsRequired();
            builder.Property(i => i.CreatedOn).IsRequired().HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

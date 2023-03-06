using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SovosCase.Application.Interfaces.Mongo;
using SovosCase.Application.Interfaces.Sql;
using SovosCase.Persistence.Contexts;
using SovosCase.Persistence.Repositories.Mongo;
using SovosCase.Persistence.Repositories.Sql;

namespace SovosCase.Persistence
{
    public static class ServiceRegistration
    {
        public static void PeristenceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SovosCaseDbContext>(options => options.UseSqlServer(configuration["MsSqlDbSettings:ConnectionString"]));
            services.AddScoped<IInvoiceMongoRepository, InvoiceMongoRepository>();
            services.AddScoped<IInvoiceSqlRepository, InvoiceSqlRepository>();
            services.AddScoped<IInvoiceItemSqlRepository, InvoiceItemSqlRepository>();
        }
    }
}

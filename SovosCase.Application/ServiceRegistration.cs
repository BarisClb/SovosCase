using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SovosCase.Application.Interfaces;
using SovosCase.Application.Services;
using SovosCase.Application.Settings;
using System.Reflection;

namespace SovosCase.Application
{
    public static class ServiceRegistration
    {
        public static void ApplicationServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddScoped<IInvoiceSqlService, InvoiceSqlService>();
            services.AddScoped<IJobService, JobService>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.Configure<MsSqlDbSettings>(configuration.GetSection("MsSqlDbSettings"));
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
        }
    }
}

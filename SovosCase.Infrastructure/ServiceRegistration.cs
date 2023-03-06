using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SovosCase.Application.Interfaces;
using System.Reflection;

namespace SovosCase.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void InfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddScoped<IEmailService, EmailService.EmailService>();
        }
    }
}

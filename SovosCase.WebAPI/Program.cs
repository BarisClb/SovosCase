using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using SovosCase.Application;
using SovosCase.Application.Settings;
using SovosCase.Infrastructure;
using SovosCase.Persistence;
using SovosCase.Persistence.Contexts;
using SovosCase.WebAPI;
using SovosCase.WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining(typeof(SovosCase.Infrastructure.FluentValidation.CreateInvoiceRegisterCommandRequestValidator)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog(SeriLogger.Configure);
builder.Services.PeristenceServiceRegistration(builder.Configuration);
builder.Services.InfrastructureServiceRegistration(builder.Configuration);
builder.Services.ApplicationServiceRegistration(builder.Configuration);
builder.Services.HangfireServiceRegistration(builder.Configuration);
builder.Services.RabbitMqServiceRegistration(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("v1/swagger.json", "SovosCase");
});
app.UseAuthorization();
app.MapControllers();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<SovosCaseDbContext>();
    //context.Database.Migrate();
    RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
    context.Database.EnsureCreated();
    //try { databaseCreator.CreateTables(); } catch { } // If tables already exists, it will throw an exception.
    //context.Database.EnsureCreated();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

builder.Services.HangfireJobRegistration(builder.Configuration);

app.Run();

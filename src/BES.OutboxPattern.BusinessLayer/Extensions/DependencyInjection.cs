using BES.OutboxPattern.BusinessLayer.BackgroundServices;
using BES.OutboxPattern.BusinessLayer.Services;
using BES.OutboxPattern.DataAccessLayer;
using CustomLibrary.EFCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NET6CustomLibrary.Extensions;

namespace BES.OutboxPattern.BusinessLayer.Extensions;

public static class DependencyInjection
{
    public static void AddServicesRegistration(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        services
            .AddScoped<IEmailMessageService, EmailMessageService>()
            .AddScoped<IEmailOutbox, EmailOutbox>();

        services
            .AddSingleton<IEmailBackgroundServices, EmailBackgroundServices>();

        services
            .AddHostedService<EmailOutboxService>()
            .AddMailKitEmailSenderService(configuration)
            .AddSerilogServices()
            .AddHealthChecksUISQLServer<AppDbContext>("SQL Server", connectionString);
    }
}
using BES.OutboxPattern.BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using NET6CustomLibrary.MailKit.Services;

namespace BES.OutboxPattern.BusinessLayer.BackgroundServices;

public class EmailBackgroundServices : IEmailBackgroundServices
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public EmailBackgroundServices(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public async void SendEmailAsync()
    {
        using var scope = serviceScopeFactory.CreateScope();

        var emailService = scope.ServiceProvider.GetRequiredService<IEmailClient>();
        var emailOutboxService = scope.ServiceProvider.GetRequiredService<IEmailOutbox>();
        var allOutboxResult = await emailOutboxService.GetAllEmailMessage();

        if (allOutboxResult.Any())
        {
            foreach (var item in allOutboxResult)
            {
                var email = item.EmailMessage;
                var result = await emailService.SendEmailAsync(email.RecipientEmail, string.Empty, email.Subject, email.Message);

                if (result)
                {
                    await emailOutboxService.Update(item);
                }
            }
        }
    }
}
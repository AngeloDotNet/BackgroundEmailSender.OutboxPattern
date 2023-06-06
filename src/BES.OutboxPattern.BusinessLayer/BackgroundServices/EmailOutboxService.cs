using System.Timers;
using Microsoft.Extensions.Hosting;

namespace BES.OutboxPattern.BusinessLayer.BackgroundServices;

public class EmailOutboxService : BackgroundService
{
    private readonly IEmailBackgroundServices emailBackgroundServices;

    public EmailOutboxService(IEmailBackgroundServices emailBackgroundServices)
    {
        this.emailBackgroundServices = emailBackgroundServices;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new System.Timers.Timer
        {
            Interval = TimeSpan.FromMinutes(1).TotalMilliseconds
        };

        timer.Elapsed += Timer_ElapsedAsync;
        timer.Start();

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async void Timer_ElapsedAsync(object sender, ElapsedEventArgs e)
    {
        emailBackgroundServices.SendEmailAsync();

        await Task.Yield();
    }
}
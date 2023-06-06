namespace BES.OutboxPattern.BusinessLayer.BackgroundServices;

public interface IEmailBackgroundServices
{
    public void SendEmailAsync();
}
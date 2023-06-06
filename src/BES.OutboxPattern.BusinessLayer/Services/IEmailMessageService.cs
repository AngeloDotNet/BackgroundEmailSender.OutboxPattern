using BES.OutboxPattern.Shared.Entities;

namespace BES.OutboxPattern.BusinessLayer.Services;

public interface IEmailMessageService
{
    Task<EmailMessageDTO> AddEmailMessage(EmailMessageDTO emailMessage);
}
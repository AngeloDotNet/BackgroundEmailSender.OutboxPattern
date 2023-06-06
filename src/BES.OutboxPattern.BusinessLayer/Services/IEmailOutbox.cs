using BES.OutboxPattern.Shared.Entities;

namespace BES.OutboxPattern.BusinessLayer.Services;

public interface IEmailOutbox
{
    Task<EmailOutboxDTO> Add(EmailOutboxDTO emailOutbox);
    Task<EmailOutboxDTO> Update(EmailOutboxDTO emailOutbox);
    Task<List<EmailOutboxDTO>> GetAllEmailMessage();
}
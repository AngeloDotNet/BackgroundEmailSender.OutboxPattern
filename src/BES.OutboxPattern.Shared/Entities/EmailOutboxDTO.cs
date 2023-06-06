namespace BES.OutboxPattern.Shared.Entities;

public class EmailOutboxDTO
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public EmailMessageDTO EmailMessage { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Success { get; set; }
}
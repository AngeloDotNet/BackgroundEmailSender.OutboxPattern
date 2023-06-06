namespace BES.OutboxPattern.Shared.Entities;

public class EmailMessageDTO
{
    public Guid Id { get; set; }
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}
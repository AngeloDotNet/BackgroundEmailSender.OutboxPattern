namespace BES.OutboxPattern.Shared.Request;

public class EmailMessageRequest
{
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}
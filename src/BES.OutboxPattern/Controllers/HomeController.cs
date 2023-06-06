using System.Net;
using System.Net.Mime;
using BES.OutboxPattern.BusinessLayer.Services;
using BES.OutboxPattern.Shared.Entities;
using BES.OutboxPattern.Shared.Request;
using Microsoft.AspNetCore.Mvc;
using NET6CustomLibrary.CustomResponse;
using NET6CustomLibrary.MailKit.Services;

namespace BES.OutboxPattern.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class HomeController : ControllerBase
{
    private readonly IEmailMessageService emailMessage;
    private readonly IEmailClient emailClient;
    private readonly IEmailOutbox emailOutbox;

    public HomeController(IEmailMessageService emailMessage, IEmailClient emailClient, IEmailOutbox emailOutbox)
    {
        this.emailMessage = emailMessage;
        this.emailClient = emailClient;
        this.emailOutbox = emailOutbox;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmailMessageDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveEmailMessageAsync(EmailMessageRequest messageRequest)
    {
        var request = new EmailMessageDTO()
        {
            Id = Guid.NewGuid(),
            RecipientEmail = messageRequest.RecipientEmail,
            Subject = messageRequest.Subject,
            Message = messageRequest.Message
        };

        var result = await emailMessage.AddEmailMessage(request);

        if (result != null)
        {
            var sendEmail = await emailClient.SendEmailAsync(result.RecipientEmail, string.Empty, result.Subject, result.Message);

            if (sendEmail)
            {
                return Ok(new DefaultResponse(true, result));
            }
            else
            {
                var email = new EmailOutboxDTO
                {
                    Id = Guid.NewGuid(),
                    MessageId = result.Id,
                    EmailMessage = result,
                    CreatedDate = DateTime.UtcNow,
                    Success = false
                };

                var outbox = await emailOutbox.Add(email);

                return Ok(new DefaultResponse(true, outbox));
            }
        }

        throw new ExceptionResponse(HttpStatusCode.BadRequest, 0, "BadRequest", "Richiesta non valida");
    }
}
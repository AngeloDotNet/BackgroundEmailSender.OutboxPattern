using System.Net;
using BES.OutboxPattern.DataAccessLayer;
using BES.OutboxPattern.Shared.Entities;
using NET6CustomLibrary.CustomResponse;

namespace BES.OutboxPattern.BusinessLayer.Services;

public class EmailMessageService : IEmailMessageService
{
    private readonly AppDbContext appDbContext;

    public EmailMessageService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<EmailMessageDTO> AddEmailMessage(EmailMessageDTO emailMessage)
    {
        if (emailMessage == null)
        {
            throw new ExceptionResponse(HttpStatusCode.UnprocessableEntity, 0, "UnprocessableEntity", "Dati non validi");
        }

        try
        {
            await appDbContext.EmailMessage.AddAsync(emailMessage);
            await appDbContext.SaveChangesAsync();

            return emailMessage;
        }
        catch (Exception ex)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, 0, "BadRequest", ex.Message);
        }
    }
}
using System.Net;
using BES.OutboxPattern.DataAccessLayer;
using BES.OutboxPattern.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using NET6CustomLibrary.CustomResponse;

namespace BES.OutboxPattern.BusinessLayer.Services;

public class EmailOutbox : IEmailOutbox
{
    private readonly AppDbContext context;

    public EmailOutbox(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<EmailOutboxDTO> Add(EmailOutboxDTO request)
    {
        if (request == null)
        {
            throw new ExceptionResponse(HttpStatusCode.UnprocessableEntity, 0, "UnprocessableEntity", "Dati non validi");
        }

        try
        {
            await context.EmailOutbox.AddAsync(request);
            await context.SaveChangesAsync();

            return request;
        }
        catch (Exception ex)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, 0, "BadRequest", ex.Message);
        }
    }

    public async Task<EmailOutboxDTO> Update(EmailOutboxDTO emailOutbox)
    {
        if (emailOutbox == null)
        {
            throw new ExceptionResponse(HttpStatusCode.UnprocessableEntity, 0, "UnprocessableEntity", "Dati non validi");
        }

        try
        {
            var entity = await context.EmailOutbox.FirstOrDefaultAsync(o => o.Id == emailOutbox.Id);

            if (entity != null)
            {
                entity.Success = true;
                await context.SaveChangesAsync();
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw new ExceptionResponse(HttpStatusCode.BadRequest, 0, "BadRequest", ex.Message);
        }
    }

    public async Task<List<EmailOutboxDTO>> GetAllEmailMessage()
    {
        var result = await context.EmailOutbox
            .Include(x => x.EmailMessage)
            .OrderByDescending(x => x.CreatedDate)
            .Where(w => w.Success == false)
            .ToListAsync();

        return result;
    }
}
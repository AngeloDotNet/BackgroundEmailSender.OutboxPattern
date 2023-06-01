using BES.OutboxPattern.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BES.OutboxPattern.DataAccessLayer;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<EmailMessageDTO> EmailMessage { get; set; }
    public virtual DbSet<EmailOutboxDTO> EmailOutbox { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmailMessageDTO>(entity =>
        {
            entity.ToTable("EmailMessage");
            entity.HasKey(entity => entity.Id);
        });

        modelBuilder.Entity<EmailOutboxDTO>(entity =>
        {
            entity.ToTable("EmailOutbox");
            entity.HasKey(entity => entity.Id);
        });
    }
}
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Contexts;

// todo: make internal
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.AddInboxStateEntity(builder =>
        {
            builder.ToTable("inbox_state", "messaging");
        });
        modelBuilder.AddOutboxMessageEntity(builder =>
        {
            builder.ToTable("outbox_message", "messaging");
        });
        modelBuilder.AddOutboxStateEntity(builder =>
        {
            builder.ToTable("outbox_state", "messaging");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}

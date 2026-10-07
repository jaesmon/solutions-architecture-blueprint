using Microsoft.EntityFrameworkCore;
using Solutions.ArchitectureBlueprint.Domain.Entities;

namespace Solutions.ArchitectureBlueprint.Infrastructure.Persistence;

public sealed class OrderDbContext(DbContextOptions<OrderDbContext> options)
    : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OutboxMessage> Outbox => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(o =>
        {
            o.HasKey(x => x.Id);
            o.Property(x => x.CustomerId).HasMaxLength(100);
            o.Property(x => x.Status).HasConversion<string>();
            o.Ignore(x => x.Total);
            o.Ignore(x => x.Events);
            o.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.OrderId);
        });

        modelBuilder.Entity<OutboxMessage>(o =>
        {
            o.HasKey(x => x.Id);
            o.Property(x => x.Type).HasMaxLength(255);
        });
    }
}

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    public bool Processed { get; set; }
}
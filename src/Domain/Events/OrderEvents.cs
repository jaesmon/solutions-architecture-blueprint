namespace Solutions.ArchitectureBlueprint.Domain.Entities;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredAt { get; }
}

public sealed record OrderPlacedEvent(Guid OrderId, string CustomerId, decimal Total)
    : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}

public sealed record OrderConfirmedEvent(Guid OrderId)
    : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
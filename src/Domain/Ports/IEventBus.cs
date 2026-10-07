using Solutions.ArchitectureBlueprint.Domain.Entities;

namespace Solutions.ArchitectureBlueprint.Domain.Ports;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IDomainEvent;
}
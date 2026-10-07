using Solutions.ArchitectureBlueprint.Domain.Entities;
using Solutions.ArchitectureBlueprint.Domain.Ports;

namespace Solutions.ArchitectureBlueprint.Infrastructure.Events;

/// <summary>
/// Adaptador de ejemplo para desarrollo local. En producción usa Kafka/RabbitMQ
/// consumiendo la tabla Outbox (ver IOutboxDispatcher).
/// </summary>
public sealed class InMemoryEventBus : IEventBus
{
    public Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IDomainEvent
    {
        System.Console.WriteLine($"[EVENTBUS] {typeof(T).Name} => {System.Text.Json.JsonSerializer.Serialize(@event)}");
        return Task.CompletedTask;
    }
}
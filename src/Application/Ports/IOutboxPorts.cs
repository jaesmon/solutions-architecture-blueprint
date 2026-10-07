using Solutions.ArchitectureBlueprint.Domain.Ports;

namespace Solutions.ArchitectureBlueprint.Application.Ports;

/// <summary>
/// Puente hacia el patrón Outbox: garantiza que los eventos de dominio
/// se publiquen de forma atómica con la transacción de persistencia.
/// </summary>
public interface IOutboxDispatcher
{
    Task DispatchPendingAsync(CancellationToken ct = default);
}

public interface IOutboxRepository : IOrderRepository
{
    // Los eventos del agregado se persisten en la misma transacción.
}
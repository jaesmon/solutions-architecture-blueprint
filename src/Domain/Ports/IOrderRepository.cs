using Solutions.ArchitectureBlueprint.Domain.Entities;

namespace Solutions.ArchitectureBlueprint.Domain.Ports;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
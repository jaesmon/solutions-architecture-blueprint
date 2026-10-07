using Microsoft.EntityFrameworkCore;
using Solutions.ArchitectureBlueprint.Domain.Entities;
using Solutions.ArchitectureBlueprint.Domain.Ports;

namespace Solutions.ArchitectureBlueprint.Infrastructure.Persistence;

public sealed class EfOrderRepository(OrderDbContext db)
    : IOrderRepository
{
    public Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);

    public async Task AddAsync(Order order, CancellationToken ct = default)
    {
        await db.Orders.AddAsync(order, ct);

        // Outbox: persiste los eventos de dominio en la MISMA transacción que el agregado.
        foreach (var e in order.Events)
        {
            db.Outbox.Add(new OutboxMessage
            {
                Id = e.EventId,
                Type = e.GetType().FullName ?? e.GetType().Name,
                Payload = System.Text.Json.JsonSerializer.Serialize(e),
                OccurredAt = e.OccurredAt,
            });
        }
    }

    public Task SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}
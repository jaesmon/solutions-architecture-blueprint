using Microsoft.EntityFrameworkCore;
using Solutions.ArchitectureBlueprint.Application.Commands.CreateOrder;
using Solutions.ArchitectureBlueprint.Application.Handlers.CreateOrder;
using Solutions.ArchitectureBlueprint.Domain.Entities;
using Solutions.ArchitectureBlueprint.Infrastructure.Events;
using Solutions.ArchitectureBlueprint.Infrastructure.Persistence;
using Xunit;

namespace Solutions.ArchitectureBlueprint.Tests.Application;

public class CreateOrderFlowTests
{
    [Fact]
    public async Task Full_flow_persists_order_and_dispatches_event()
    {
        await using var db = new OrderDbContext(
            new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        var repo = new EfOrderRepository(db);
        var bus = new InMemoryEventBus();
        var handler = new CreateOrderCommandHandler(repo, bus);

        var command = new CreateOrderCommand("cust-42", new[] { new Order.OrderItemInput("SKU-A", 3) });
        var order = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, order.Id);
        Assert.Equal(OrderStatus.Created, order.Status);
        Assert.True(await db.Outbox.AnyAsync(m => m.Type.Contains(nameof(OrderPlacedEvent))));
    }
}
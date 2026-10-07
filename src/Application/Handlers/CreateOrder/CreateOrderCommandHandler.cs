using Solutions.ArchitectureBlueprint.Application.Commands.CreateOrder;
using Solutions.ArchitectureBlueprint.Domain;
using Solutions.ArchitectureBlueprint.Domain.Entities;
using Solutions.ArchitectureBlueprint.Domain.Ports;

namespace Solutions.ArchitectureBlueprint.Application.Handlers.CreateOrder;

public sealed class CreateOrderCommandHandler
{
    private readonly IOrderRepository _orders;
    private readonly IEventBus _eventBus;

    public CreateOrderCommandHandler(IOrderRepository orders, IEventBus eventBus)
    {
        _orders = orders;
        _eventBus = eventBus;
    }

    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        var order = Order.Create(command.CustomerId, command.Items);

        await _orders.AddAsync(order, ct);   // persiste agregado + eventos (Outbox)
        await _orders.SaveChangesAsync(ct);

        foreach (var e in order.Events)
            await _eventBus.PublishAsync(e, ct);

        order.ClearEvents();
        return order;
    }
}
using Solutions.ArchitectureBlueprint.Domain.Entities;

namespace Solutions.ArchitectureBlueprint.Application.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    string CustomerId,
    IReadOnlyList<Order.OrderItemInput> Items) : ICommand<Order>;
namespace Solutions.ArchitectureBlueprint.Domain.Entities;

public sealed class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public string CustomerId { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public decimal Total => _items.Sum(i => i.LineTotal);
    public IReadOnlyList<OrderItem> Items => _items;
    public DateTime CreatedAt { get; private set; }
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyList<IDomainEvent> Events => _events;

    private Order() { } // EF Core

    public static Order Create(string customerId, IEnumerable<OrderItemInput> items)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new DomainException("CustomerId es obligatorio.");
        if (!items.Any())
            throw new DomainException("Una orden requiere al menos un ítem.");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Status = OrderStatus.Created,
            CreatedAt = DateTime.UtcNow,
        };
        order._items.AddRange(items.Select(i => new OrderItem(order.Id, i.Sku, i.Quantity)));
        order.AddEvent(new OrderPlacedEvent(order.Id, order.CustomerId, order.Total));
        return order;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Created)
            throw new DomainException("Solo se puede confirmar una orden creada.");
        Status = OrderStatus.Confirmed;
        AddEvent(new OrderConfirmedEvent(Id));
    }

    private void AddEvent(IDomainEvent e) => _events.Add(e);
    public void ClearEvents() => _events.Clear();

    public record OrderItemInput(string Sku, int Quantity);
}

public enum OrderStatus
{
    Created,
    Confirmed,
    Cancelled,
}

public sealed record OrderItem(Guid OrderId, string Sku, int Quantity)
{
    public decimal LineTotal => Quantity * 10m; // precio de ejemplo: usar SKU real
}
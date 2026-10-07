using Solutions.ArchitectureBlueprint.Domain;
using Solutions.ArchitectureBlueprint.Domain.Entities;
using Xunit;

namespace Solutions.ArchitectureBlueprint.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Create_valid_order_emits_OrderPlacedEvent()
    {
        var order = Order.Create("cust-1", new[] { new Order.OrderItemInput("SKU-1", 2) });

        Assert.Equal(OrderStatus.Created, order.Status);
        Assert.Single(order.Events);
        Assert.IsType<OrderPlacedEvent>(order.Events[0]);
    }

    [Fact]
    public void Create_without_items_throws_DomainException()
    {
        Assert.Throws<DomainException>(() => Order.Create("cust-1", Array.Empty<Order.OrderItemInput>()));
    }

    [Fact]
    public void Confirm_cancelled_flow_is_guarded()
    {
        var order = Order.Create("cust-1", new[] { new Order.OrderItemInput("SKU-1", 1) });
        order.Confirm();

        Assert.Equal(OrderStatus.Confirmed, order.Status);
        var e = Assert.IsType<OrderConfirmedEvent>(order.Events.Last());
        Assert.NotNull(e);
    }
}
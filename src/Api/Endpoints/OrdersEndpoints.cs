using Solutions.ArchitectureBlueprint.Application.Commands.CreateOrder;
using Solutions.ArchitectureBlueprint.Application.Handlers.CreateOrder;

namespace Solutions.ArchitectureBlueprint.Api.Endpoints;

public static class OrdersEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/orders", async (CreateOrderCommand body, CreateOrderCommandHandler handler, CancellationToken ct) =>
        {
            var order = await handler.Handle(body, ct);
            return Results.Created($"/orders/{order.Id}", new { order.Id, order.CustomerId, order.Total });
        })
        .WithName("CreateOrder")
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Crea una orden",
            Description = "Registra una orden aplicando las reglas de negocio del dominio. Los eventos de dominio se publican vía Outbox."
        });
    }
}
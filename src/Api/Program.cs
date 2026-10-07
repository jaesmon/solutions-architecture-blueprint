using Microsoft.EntityFrameworkCore;
using Solutions.ArchitectureBlueprint.Api.Endpoints;
using Solutions.ArchitectureBlueprint.Domain.Ports;
using Solutions.ArchitectureBlueprint.Infrastructure.Events;
using Solutions.ArchitectureBlueprint.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Composición de adaptadores en los puertos del dominio (Arquitectura Hexagonal).
builder.Services.AddDbContext<OrderDbContext>(o => o.UseInMemoryDatabase("orders"));
builder.Services.AddScoped<IOrderRepository>(sp =>
    new EfOrderRepository(sp.GetRequiredService<OrderDbContext>()));
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapOrderEndpoints();
app.Run();

public partial class Program;
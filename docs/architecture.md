# Arquitectura (C4)

## Contexto (C1)

```mermaid
flowchart LR
    U[Usuarios / Apps cliente] -->|HTTPS + OpenAPI| API[API Gateway / Api de entrada]
    API --> BL[[Sistemas de negocio: ERP, CRM, Facturación]]
```

## Contenedores (C2)

```mermaid
flowchart LR
    subgraph Boundary["Límite hexagonal"]
        API[Api<br/>Minimal API .NET] --> CQRS[CQRS<br/>Commands / Queries]
        CQRS --> APP[Casos de uso<br/>Application/Middle]
    end
    APP -->|puerto| DOM[(Dominio<br/>aggregates + eventos)]
    APP --> OUTBOX[Outbox]
    OUTBOX --> BUS[[Event Bus KAFKA/RabbitMQ]]
    API --> DB[(Postgres/SQL Server<br/>EF Core)]
    APP --> OBS[[OpenTelemetry Collector]]
    OBS --> GRAFANA[Grafana / Jaeger]
```

## Componentes (C3) — flujo `CreateOrder`

```mermaid
sequenceDiagram
    participant C as Cliente
    participant A as Api
    participant H as CreateOrderHandler
    participant D as Domain (Order)
    participant R as OrderRepository
    participant B as EventBus

    C->>A: POST /orders
    A->>H: CreateOrderCommand
    H->>D: Order.Create(...)  (reglas de negocio)
    D-->>H: OrderPlacedEvent
    H->>R: Save (con Outbox)
    R-->>H: ok
    H->>B: Dispatch event
    H-->>A: 201 Created
```

## Reglas de oro del blueprint

1. **El dominio no conoce nada de infraestructura** — solo `Domain/` puro.
2. **Los casos de uso dependen de interfaces (puertos)**, nunca de implementaciones.
3. **La entrada y la salida son adaptadores** que se conectan por composición en `Program.cs`.
4. **Cada comando/usecase es testeable en aislamiento** (protocolo de la Arquitectura Hexagonal).
5. **Persistencia atómica con Outbox** para no perder eventos (garantía de consistencia eventual).
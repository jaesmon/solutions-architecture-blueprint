# Solutions Architecture Blueprint

> Plantilla de referencia para construir sistemas **DDD + Arquitectura Hexagonal + CQRS/Event Sourcing** en .NET. Nace del SDK corporativo que estandarizó la producción de software en una organización de +500 desarrolladores.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

## Propósito del blueprint

El objetivo no es "escribir código rápido", sino **destinar el futuro del proyecto** desde la arquitectura. Este template desacopla el dominio del negocio de cualquier marco técnico (EF Core, Kafka, cloud), de modo que:

- El dominio se pruebe de forma **pura**, sin infraestructura.
- Los adaptadores (DB, mensajería, HTTP, cloud) se conecten en los "puertos" laterales.
- Los casos de uso usen **CQRS** (commands/queries separados) y **Event Sourcing** opcional.
- Se apliquen atributos de calidad: seguridad, observabilidad (OpenTelemetry), resiliencia.

## Estructura

```
solutions-architecture-blueprint/
├── docs/                     # Arquitectura (C4), ADRs, guías
│   ├── architecture.md
│   ├── ADR-0001-hexagonal-and-ddd.md
│   └── getting-started.md
├── src/
│   ├── Domain/               # Aggregate roots, entidades, value objects, eventos de dominio
│   ├── Application/          # Casos de uso CQRS (handlers), puertos
│   ├── Infrastructure/       # Adaptadores: EF Core, Event Bus, Outbox, Observabilidad
│   └── Api/                  # Minimal API / controladores (entrada hexagonal)
├── tests/
│   ├── Domain.UnitTests/             # Tests puros del dominio
│   ├── Application.IntegrationTests/ # Flujos con adaptadores reales (DB en contenedor)
│   └── Api.ContractTests/            # Contract testing del API (SDD)
├── docker-compose.yml        # Postgres + Kafka/RabbitMQ locales
└── Directory.Build.props
```

## Quickstart

```bash
dotnet restore
dotnet build
dotnet test
docker compose up -d   # dependencias locales
dotnet run --project src/Api
```

Consulta [docs/getting-started.md](docs/getting-started.md) para el flujo paso a paso.

## Decisiones clave (ADR)

Este repo documenta cada decisión de arquitectura en `docs/`:

| ADR | Decisión |
|-----|----------|
| [ADR-0001](docs/ADR-0001-hexagonal-and-ddd.md) | Dominio desacoplado con Arquitectura Hexagonal y DDD |

## Cómo usarlo como base para tu propio SDK corporativo

1. Copia este repo como `dotnet new` a tu medida (o adapta `Directory.Build.props`).
2. Sustituye el agregado `Order` de ejemplo por tu dominio real.
3. Publica el API con contratos OpenAPI y genera el cliente desde la spec (ver [`spec-driven-development`](https://github.com/jaesmon/spec-driven-development)).
4. Conecta observabilidad (OpenTelemetry) y DevSecOps (SonarQube, SBOM).

## Licencia

MIT — ver [LICENSE](LICENSE).
# Quickstart

## Requisitos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (para dependencias locales)
- Opcional: [OpenAPI Generator](https://openapi-generator.tech/) o el CLI de `spec-driven-development` para generar clientes.

## 1. Restaurar y compilar

```bash
dotnet restore
dotnet build
```

## 2. Levantar dependencias locales

```bash
docker compose up -d
```

## 3. Ejecutar tests

```bash
dotnet test
```

Los tres proyectos de test son la "red de seguridad" del blueprint:
- `Domain.UnitTests` → reglas de negocio puras.
- `Application.IntegrationTests` → flujo completo con Postgres real (contenedor).
- `Api.ContractTests` → valida que el API cumple el contrato OpenAPI.

## 4. Correr la API

```bash
dotnet run --project src/Api
```

Ejemplo:

```bash
curl -X POST http://localhost:5000/orders \
  -H "Content-Type: application/json" \
  -d '{"customerId":"cust-123","items":[{"sku":"SKU-1","quantity":2}]}'
```

## 5. Adaptar a tu dominio

1. Sustituye el agregado `Order` por tu agregado real en `src/Domain/`.
2. Define tus comandos/queries en `src/Application/`.
3. Publica el contrato OpenAPI y genera el cliente (→ `spec-driven-development`).
4. Agrega observabilidad: `src/Infrastructure/Observability/` ya prepara OpenTelemetry.
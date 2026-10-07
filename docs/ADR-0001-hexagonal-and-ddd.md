# ADR-0001 — Arquitectura Hexagonal + DDD como base del blueprint

- **Estado:** Aceptado
- **Fecha:** 2026-01

## Contexto

La organización entregaba software acoplado a frameworks y bases de datos. Cambiar de storage o de librería implicaba reescribir lógica de negocio. Los equipos confundían "ser ágiles" con "programar rápido", generando parches técnicos.

## Decisión

Adoptar:
- **Domain-Driven Design (DDD)** para el lenguaje ubicuo y los agregados como unidad de consistencia.
- **Arquitectura Hexagonal (Ports & Adapters)** para aislar el dominio y los casos de uso.
- **CQRS** para separar lectura/escritura y liberar el modelo transaccional.
- **Event Sourcing** (opcional, por agregado) cuando el historial sea un requisito de negocio.

## Consecuencias

- ✅ El dominio se puede probar sin infraestructura.
- ✅ Adoptar nuevas tecnologías (storage, bus, cloud) es agregar un adaptador.
- ⚠️ Curva de aprendizaje; mitigada con este blueprint y el SDK corporativo.
- ⚠️ Mayor ceremonía inicial; mitigada con generación desde specs (SDD).

## Alternativas consideradas

- **Layered clásico:** menor esfuerzo inicial, pero acopla dominio a infraestructura.
- **Clean Architecture (solo):** buena, complementada aquí con CQRS/Eventos explícitos.
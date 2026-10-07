# Contributing

¡Gracias por aportar al blueprint!

- Usa [Conventional Commits](https://www.conventionalcommits.org) (`feat`, `fix`, `docs`, `test`).
- Todo cambio de arquitectura requiere un nuevo ADR en `docs/`.
- Mantén el dominio puro: `src/Domain/` no puede referenciar infraestructura.
- Agrega o actualiza tests antes de aceptar un PR.

Flujo:

1. Fork + rama `feat/<nombre>`.
2. Implementa con su test.
3. Abre el PR con descripción del ADR afectado (si aplica).
# 🧩 DDD Sample API

API REST desarrollada en **.NET 8**, aplicando **Domain-Driven Design (DDD)** y **Clean Architecture**, con autenticación basada en **JWT**.
El proyecto sirve como base escalable, mantenible y preparada para entornos empresariales.

---

## 📌 Descripción

Esta API implementa una arquitectura desacoplada que separa claramente el dominio, la aplicación, la infraestructura y la capa de presentación.
Se aplican principios de **Clean Architecture** y **DDD**, permitiendo alta cohesión, bajo acoplamiento y facilidad de pruebas.

---

## 🚀 Tecnologías

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT (JSON Web Token)
- Swagger / OpenAPI
- Clean Architecture
- Domain-Driven Design (DDD)

---

## 🏗️ Arquitectura

```
src/
 ├── DDD.Api
 │   └── Controllers, Middlewares, Configuración
 ├── DDD.Application
 │   └── Commands, Queries, DTOs, Interfaces
 ├── DDD.Domain
 │   └── Entities, ValueObjects, Repositories
 └── DDD.Infrastructure
     └── Persistence, EF Core, Repositories
```

### Principios aplicados
- Separación de responsabilidades
- Inversión de dependencias
- Dominio independiente de frameworks
- Commands para escritura
- Queries para lectura

---

## 🔐 Autenticación (JWT)

La API utiliza autenticación basada en **JWT**.

### Obtener token
```
POST /api/auth/login
```

Body:
```json
{
  "email": "admin@test.com",
  "password": "123456"
}
```

Enviar el token en los endpoints protegidos:
```
Authorization: Bearer {token}
```

---

## 📦 Endpoints principales

### Clients
- POST `/api/clients`
- GET `/api/clients`
- PUT `/api/clients/{id}`
- DELETE `/api/clients/{id}`

### Orders
- POST `/api/orders`
- GET `/api/orders`
- GET `/api/orders/{id}`

La documentación completa se encuentra disponible en **Swagger**.

---

## ⚙️ Configuración

Editar `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DDD_DB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "SUPER_SECRET_KEY",
    "Issuer": "DDD.Api",
    "Audience": "DDD.Api.Users"
  }
}
```

---

## ▶️ Cómo ejecutar el proyecto

1. Restaurar dependencias:
```
dotnet restore
```

2. Ejecutar migraciones:
```
dotnet ef database update
```

3. Ejecutar la API:
```
dotnet run --project src/DDD.Api
```

4. Acceder a Swagger:
```
https://localhost:{port}/swagger
```

---

## 🧠 Notas finales

- Arquitectura preparada para escalar
- Fácil integración con API Gateway
- Base sólida para microservicios

---

👨‍💻 Autor: Alejandro

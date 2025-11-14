# TekusServicesAPI

Este proyecto es una API, desarrollada en **.NET Core 8.0** con una arquitectura modular. El proyecto está organizado en capas para mantener la separación de responsabilidades y facilitar la escalabilidad y mantenibilidad.

## Estructura del Proyecto
```
│── API
│   ├── TekusServices.API
│   ├── Controllers       Endpoints expuestos al cliente
│   ├── Extensions        Métodos de extensión
│   ├── appsettings.json  Configuración de la aplicación
│   ├── Program.cs        Punto de entrada principal de la API
│
│── Application
│   ├── TekusServices.Application
│   ├── DTO               Objetos de transferencia de datos
│   ├── Interfaces        Interfaces de servicios que definen el contrato
│   ├── Mappings          Configuración de AutoMapper
│   ├── Services          Implementación de la lógica de aplicación
│   ├── Utils             Clases auxiliares (Jwt)
│
│── Domain
│   ├── TekusServices.Domain
│   ├── Entities          Entidades del dominio
│   ├── Interfaces        Interfaces del dominio
│
│── Infrastructure
│   ├── TekusServices.Infrastructure
│   ├── Data              Contexto de base de datos (EF Core DbContext)
│   ├── Repositories      Implementaciones concretas de acceso a datos
│   ├── Migrations        Migraciones de Entity Framework Core
```

## Autenticación con JWT
El proyecto implementa autenticación basada en JSON Web Tokens (JWT) para proteger los endpoints de la API y gestionar el acceso de los usuarios de forma segura.

El inicio de sesión se realiza mediante el endpoint:

**POST /api/Users/login**

Este endpoint valida las credenciales del usuario y devuelve un token JWT

Los endpoints protegidos utilizan el atributo **[Authorize]**

## Funcionamiento
- Antes de ejecutar el proyecto, configurar la conexión a la base de datos "SQLConnection" en appsettings.json
- Al ejecutar la aplicación se ejecuta la migracion "InitialTables" la cual crea la base de datos con sus 
  respectivas tablas.
- También se crean datos de ejemplos en las tablas Providers, ProviderCustomFields, Services y Users

<img width="770" height="640" alt="image" src="https://github.com/user-attachments/assets/d903b40d-4727-412a-8120-1651a5509b66" />



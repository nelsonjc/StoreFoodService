# Store Food Service

## Contexto de la Aplicación

He completado el desarrollo de la API para la tienda de alimentos en línea según los requisitos especificados. A continuación, detallo cómo se abordaron los diferentes aspectos:

### Registro de Usuarios

- Se implementó un sistema de registro de usuarios que incluye validación de campos y almacenamiento seguro de contraseñas utilizando hashing y salting.

### Catálogo de Productos

- El catálogo de productos se encuentra implementado en la base de datos y se puede acceder a él mediante la API para consultar información sobre frutas, verduras y otros alimentos disponibles en la tienda.

### Realización de Pedidos

- Los usuarios autenticados pueden realizar pedidos a través de la API. Se aplicaron validaciones para asegurar que la cantidad de productos solicitada esté dentro del stock disponible.

### Sistema de Administración

- Se desarrolló un sistema de administración para que los administradores puedan gestionar los productos disponibles, agregar nuevos productos y visualizar los pedidos realizados por los usuarios.

### Tecnologías Utilizadas

Se emplearon las siguientes tecnologías y herramientas durante el desarrollo:

- Lenguaje de Programación: C#
- Framework: .NET
- Base de Datos: SQL Server
- Autenticación y Autorización: JWT
- Pruebas Unitarias: NUnit y Moq
- Base de datos con Tablas, SPs y Vistas
- .NET 8
- Dapper
- Automapper
- Swagger
- Azure KeyVault
- Azure Identity
- Azure Application Insights
- Net. Mail

## Patrones de Diseño Implementados

- Inyección de Dependencias (DI)
- Patrón de Repositorio
- Patrón de Diseño SOLID
- Middleware
- Mapeo Objeto-Relacional (ORM)

## Arquitectura Orientada al Dominio

Se aplicó la arquitectura orientada al dominio de la manera más completa posible en el proyecto. Las capas del proyecto son las siguientes:

1. **ShopFood.API:** Contiene la configuración inicial del proyecto, servicios configurados, cadenas de conexión y controladores HTTP para la API.
2. **ShopFood.Application:** Contiene la lógica de negocio, validaciones y wrappers.
3. **ShopFood.Domain:** Es el corazón de la aplicación, contiene los DTOs, Entidades, Helpers, Interfaces, Templates, Utils, Variables, Enums y constantes que son transversales al proyecto.
4. **ShopFood.Infraestructure:** Contiene la capa de acceso a datos.
5. **ShopFood.Tests:** Proyecto configurado para realizar las pruebas unitarias del proyecto.

Es muy importante tener en cuenta la separación de capas, la inyección de dependencias, las pruebas unitarias y un modelado de dominio que refleje los aspectos fundamentales de la lógica de negocio.

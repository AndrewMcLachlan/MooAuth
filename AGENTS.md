# Overview

MooAuth is a browser-based application to provide authorisation-as-a-service. MooAuth does not issue tokens or handle authentication - it is purely an authorisation service that tells consuming applications what permissions a user has.

Consuming applications integrate with MooAuth by calling the Authorisation API with a user's JWT token. The API returns that user's permissions for the calling application.

# Key Concepts

- **Application** - A grouping of permissions that apply to a specific application.
- **Permission** - A representation of a permission in an application (e.g., read, write). Permissions are added to Roles.
- **Resource** - An identifier for an entity that can be secured with permissions. To MooAuth, a Resource is just a Data Source Key and a string representing the ID of the resource (or an expression that can be resolved by the consuming Application). MooAuth treats this as an opaque string. Resources are separate from Applications.
- **Role** - A collection of permissions. A Role and an Actor are combined with one or more Resources to define what permissions the Actor has on those Resources. Roles can span multiple Applications.
- **Actor** - A user or group assigned roles for specific resources.
- **Connector** - An integration to a third-party authentication provider. Connectors validate tokens and retrieve user/group information, but authentication itself is handled entirely by the third-party provider.
- **Data Source** - An integration to a source of resource definitions.

## Workflow

### Admin Setup (via MooAuth UI)
1. An admin logs into MooAuth and adds a connector (integration with their authentication provider).
2. The admin optionally creates data sources. A data source can be:
    - A free-text string
    - A pick-list defined in MooAuth
    - A pick-list retrieved from a third-party API (endpoint and auth configured in MooAuth)
3. The admin creates an application, permissions, and roles.
4. The admin assigns roles to users and groups, optionally using a data source to specify resources.

### Runtime Authorisation
When an application needs to check a user's permissions:
1. The application calls the Authorisation API with the user's JWT token and the Application ID (as a path parameter).
2. The Authorisation API extracts the user's ID and group IDs from the JWT claims. These Actor IDs are stored in the `ActorRoleResource` table.
3. The API returns a dictionary where:
   - Keys are permission name strings
   - Values are lists of Resource key/value pairs
   - Where a permission is granted through multiple roles with different resources, the entries are merged

No additional authentication is required beyond the user's JWT - the consuming application does not need API keys or client credentials.

**Note:** MooAuth is single-tenant. There is no multi-tenancy support.

# Architecture

## Technology Stack

The technology stack for MooAuth includes:
- **Frontend**: React.js (TypeScript) for building a responsive and interactive user interface.
- **Backend**: .NET / ASP.NET Core. MooAuth will generally use the latest general-availability version of .NET (currently .NET 9, targeting .NET 10).
- **Database**: Azure SQL Server with Entity Framework Core as the ORM.
- **API Documentation**: Microsoft.AspNetCore.OpenApi (not Swashbuckle) with Swagger UI for interactive documentation.

## Software Design Patterns

The software design patterns employed in MooAuth include:

- **Minimal API** - Endpoints are defined using ASP.NET Core Minimal APIs.
- **Command Query Responsibility Segregation (CQRS)** - Commands (write operations) and Queries (read operations) are separated.
- **Domain-Driven Design (DDD)** - Business logic is encapsulated in domain entities with rich behavior.
- **Unit of Work and Repository Patterns** - For managing database transactions and data access.
- **Specification Pattern** - Used for composable query logic in repositories.
- **Domain Events** - For decoupled communication between aggregates.

## Code Layout (Backend)

The codebase is organized into the following main projects:

### Core Projects
- **Asm.MooAuth.Authorisation.Api**: The API applications will call to authorise their users
- **Asm.MooAuth.Web.Api**: The ASP.NET Core Web API project. This is intentionally kept minimal, with most business logic in other projects.
- **Asm.MooAuth.Domain**: Contains the core domain logic, including entities, value objects, aggregates, specifications, domain events, and repository interfaces. It specifically avoids dependencies on infrastructure concerns.
- **Asm.MooAuth.Infrastructure**: Contains implementations for data access, including database context, repository implementations, entity configurations, and migrations.
- **Asm.MooAuth.Models**: DTOs and models for API requests/responses.
- **Asm.MooAuth**: Core business services and utilities.

### Module Projects
- **Asm.MooAuth.Modules.***: A set of projects that encapsulate specific business capabilities. Each module contains:
  - Commands (write operations) with their handlers
  - Queries (read operations) with their handlers
  - Endpoint mappings
  - Module registration
  - Module-specific models (DTOs)

### Connector Integration Projects
- **Asm.MooAuth.Connector.***: Projects for integrating with specific authentication / user providers (e.g., Entra, Auth0 etc.).
  - A connector can:
    - Validate tokens presented to the MooAuth API for authentication with MooAuth itself
    - Retrieve user and group information from the third-party provider

### Secrets Integration Projects
- **Asm.MooAuth.Secrets.***: MooAuth stores generated secrets (e.g., client secrets). These projects integrate with third-party secret management systems (e.g., Azure Key Vault, AWS Secrets Manager).

### Frontend
- **Asm.MooAuth.Web.App**: React/TypeScript single-page application located in `src/Asm.MooAuth.Web.App`.

### Database
- **Asm.MooAuth.Database**: SQL Server Database Project (SDK-style) containing schemas, tables, functions, and stored procedures.

## Libraries and Frameworks

### Backend

- **Entity Framework Core**: An Object-Relational Mapper (ORM) for database access.
- **ASM Library**: A set of custom NuGet packages developed to hide boilerplate and simplify common tasks:
    - `Asm.AspNetCore.Api` - For API-specific functionality including OpenAPI configuration.
    - `Asm.AspNetCore` - For application bootstrapping, including OpenTelemetry and health checks.
    - `Asm.AspNetCore.Modules` - Module registration and endpoint mapping infrastructure.
    - `Asm.Cqrs.AspNetCore` - Extensions that allow endpoints to be mapped to CQRS commands and queries.
    - `Asm.Domain` / `Asm.Domain.Infrastructure` - Base classes and interfaces for implementing DDD patterns.
    - `Asm.OAuth` - OAuth/OIDC configuration helpers.
  
  The code for the ASM Library is available at: https://github.com/AndrewMcLachlan/ASM

- **ReqnRoll**: A behavior-driven development (BDD) framework for writing human-readable integration tests.
- **Database Project**: The database design is maintained as a first-class component using the newer SDK-style SQL project format.

### Frontend

- **React.js**: A JavaScript library for building user interfaces (using TypeScript).
- **React Router**: A library for routing in React applications.
- **MSAL (Microsoft Authentication Library)**: For handling authentication and authorization with Azure AD.
- **React Query (TanStack Query)**: For server state management and API calls.
- **MooApp**: A set of custom packages of React components and hooks to simplify application development.
    - **@andrewmclachlan/moo-ds** - An opinionated component library, based on Bootstrap and React-Bootstrap
    - **@andrewmclachlan/moo-app** - Provides app-level components such as:
        - An application bootstrapper
        - A layout
        - Authentication with MSAL
        - API management with React Query and Axios, with custom React Query hooks for different HTTP verbs
    

The code for MooApp is available at: https://github.com/AndrewMcLachlan/MooApp

## Database Design
The database schema is defined in the `Asm.MooAuth.Database` project using a SQL Server Database Project. Key design principles include:
- **Normalization**: The schema is normalized to reduce redundancy and improve data integrity.
- **Enums as Tables**:100: C# Enums are represented as lookup tables for flexibility.
- **Primary Keys**: The ID of a table is typically a GUID, but can be an INT for lookup tables or performance-sensitive tables. The name of the ID column is always `Id`.
- **Seed Data**: Lookup tables are seeded with initial data using post-deployment scripts. These scripts use MERGE statements to avoid duplication.

## CQRS Implementation Guidelines

### Commands
- Commands represent write operations (Create, Update, Delete).
- Located in `Commands/` folders within module projects.
- Must implement `ICommand<TResult>`.
- Handlers must implement `ICommandHandler<TCommand, TResult>`.
- Can use custom model binding via `BindAsync` methods.
- Should validate business rules and throw appropriate exceptions (`NotFoundException`, `BadHttpRequestException`).

### Queries
- Queries represent read operations.
- Located in `Queries/` folders within module projects.
- Must implement `IQuery<TResult>`.
- Handlers must implement `IQueryHandler<TQuery, TResult>`.
- Should use specifications for complex filtering.
- Can include pagination parameters (PageSize, PageNumber).

### Endpoints
- Defined in `Endpoints/` folders within module projects.
- Use Minimal API syntax with `.MapCommand()` and `.MapQuery()` extensions from `Asm.Cqrs.AspNetCore`.
- Apply `.WithOpenApi()` for API documentation.
- Apply authorization policies using `.RequireAuthorization()`.

## Domain Model Guidelines

### Entities
- Located in `Asm.MooAuth.Domain/Entities/`.
- Should inherit from appropriate base classes (`Entity<TId>`).
- Should use the `[AggregateRoot]` attribute for aggregate roots.
- Contain business logic and invariants.
- Use domain events for cross-aggregate communication.
- Expose behavior through methods, not just property setters.

### Specifications
- Used for complex query logic and filtering.
- Located in `Specifications/` folders under entity folders.
- Implement `ISpecification<T>`.
- Should be composable and reusable.
- Common examples: `FilterSpecification`, `IncludeXxxSpecification`, `SortSpecification`.

### Repositories
- Interface defined in `Asm.MooAuth.Domain/Entities/`.
- Implementation in `Asm.MooAuth.Infrastructure/Repositories/`.
- Should work with aggregate roots, not individual entities.
- Use specifications for complex queries.

## Hosting

The application is hosted on Microsoft Azure using the following services:
- **Azure App Service**: For hosting the web application and API.
    - **Deployment Slots**: Used for staging and production environments to facilitate smooth deployments.
- **Azure SQL Database**: For the application database.
- **Azure WebJobs**: For background job processing.
- **Key Vault**: For securely storing application secrets and configuration settings.
- **Application Insights**: For monitoring and telemetry.

## Build and Deployment

- The build and deployment workflow is defined in `.github/workflows/build.yml`.
- Uses GitHub Actions for CI/CD.
- Automated deployment to Azure App Service.
- Database migrations are applied automatically during deployment.

## Testing

- **Unit/Integration Tests**: Located in `tests/` directory.
- **BDD Tests**: Using ReqnRoll with `.feature` files for human-readable specifications.
- Test projects mirror the structure of the main projects.
- Use mocks and fakes for external dependencies.

## Common Patterns and Conventions

### Naming Conventions
- **Entities**: PascalCase singular (e.g., `Connector`, `Permission`).
- **Collections**: Use plural (e.g., `Permissions`, `Roles`).
- **Commands**: Verb-based names (e.g., `Create`, `Update`, `Delete`, `Close`).
- **Queries**: Descriptive names (e.g., `Get`, `GetAll`).
- **DTOs/Models**: Match entity names but in the Models namespace.

### File Organization
- Group related files by feature/entity, not by type.
- Commands, Queries, and Models specific to a feature should be in that feature's folder.
- Shared models belong in `Asm.MooAuth.Models`.

### Error Handling
- Use `NotFoundException` when a requested resource doesn't exist.
- Use `BadHttpRequestException` for invalid request data.
- Use `UnauthorizedAccessException` or authorization policies for security violations.
- Domain validation should throw domain-specific exceptions.

### API Design
- Use RESTful conventions for endpoint URLs.
- Group endpoints logically (e.g., `/api/accounts/{id}/virtual`, `/api/instruments/{id}/import`).
- Use proper HTTP verbs (GET, POST, PATCH, DELETE).
- Return appropriate status codes.
- Include OpenAPI documentation attributes.

### Database Migrations
- Database schema is defined in the Database Project.
- Changes should be made to SQL files, not generated via EF migrations.
- Use database project deployment for schema updates.

# Important Technical Notes

## OpenAPI Generation
- The project uses `Microsoft.AspNetCore.OpenApi` (not Swashbuckle) for OpenAPI document generation.
- OpenAPI documents are generated at build time via `Microsoft.Extensions.ApiDescription.Server`.
- **Do not add `Swashbuckle.AspNetCore` as it conflicts with build-time generation**.
- Security schemes (OIDC) must handle null configuration gracefully for build-time generation.

## Module Registration
- Modules must implement `IModule` from `Asm.AspNetCore.Modules`.
- Register in `Program.cs` via `builder.RegisterModules()`.
- Each module's `AddServices()` method registers its dependencies.
- Each module's `MapEndpoints()` method defines its API endpoints.

## Data Access
- Always use the Unit of Work pattern for transactions.
- Repositories should only be used for aggregate roots.
- Use specifications for complex queries.
- Navigation properties should be explicitly loaded via specifications.
- Avoid N+1 query problems by using `.Include()` or specifications.

### IQueryable vs Repository

The infrastructure layer provides two ways to access entities:

1. **`IQueryable<TEntity>`** - Injected directly for **read-only queries**
   - Configured with `AsNoTracking()` for optimal performance
   - Entities retrieved this way are **not tracked** by EF Core's change tracker
   - **Use in Query handlers only** - perfect for fast, read-only operations
   - Cannot be used to update entities (changes won't be persisted)
   - Example: `IQueryable<ForecastPlan> plans`

2. **`IRepository<TEntity>`** - Injected for **commands that modify data**
   - Entities are tracked by EF Core's change tracker
   - Changes to entities will be persisted when `IUnitOfWork.SaveChangesAsync()` is called
   - **Use in Command handlers** that create, update, or delete entities
   - Supports specifications for eager loading: `repository.Get(id, specification, cancellationToken)`
   - Example: `IForecastRepository forecastRepository`

**Rule of thumb:**
- **Queries** (read operations) → Use `IQueryable<TEntity>`
- **Commands** (write operations) → Use `IRepository<TEntity>`

```csharp
// Query handler - uses IQueryable (no tracking, read-only)
internal class GetRoleHandler(IQueryable<Role> roles, ...) : IQueryHandler<GetRole, Role>
{
    public async ValueTask<Role> Handle(GetRole query, ...)
    {
        var role = await roles
            .Apply(new RoleDetailsSpecification())
            .SingleAsync(p => p.Id == query.Id, cancellationToken);
        return plan.ToModel();
    }
}

// Command handler - uses Repository (tracked, can modify)
internal class UpdateRoleHandler(IRoleRepository roleRepository, ...) : ICommandHandler<UpdateRole, Role>
{
    public async ValueTask<Role> Handle(UpdateRole request, ...)
    {
        var entity = await roleRepository.Get(request.Id, new RoleDetailsSpecification(), cancellationToken);
        entity.Name = request.Role.Name; // Changes will be tracked
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToModel();
    }
}
```

## Domain Events
- Raised within aggregate roots during state changes.
- Handled by event handlers in the infrastructure layer.
- Used for decoupled communication between aggregates.
- Example: `RoleAddedEvent` triggers balance updates.

# Instructions for AI Agents

When contributing to the MooAuth codebase:

1. **Read the AGENTS.md file first** to understand the architecture and patterns.
2. **Follow the established patterns**: CQRS, DDD, specifications, modules.
3. **Respect the folder structure**: Place files in the correct project and folder.
4. **Use the ASM library conventions**: Commands, Queries, and their handlers.
5. **Maintain separation of concerns**: Domain logic in Domain, infrastructure in Infrastructure, API concerns in Web.Api.
6. **Write clear, descriptive names** for classes, methods, and variables.
7. **Use domain-driven language** that matches the business concepts.
8. **Add appropriate tests** for new features (BDD tests for features, unit tests for complex logic).
9. **Follow C# coding conventions** as defined in `.editorconfig`.
    - **Use Framework types correctly** (e.g. `string` for declarations and `String` for static methods).
10. **No Warnings**: Ensure code compiles without warnings.
11. **Document API endpoints** with OpenAPI attributes.
12. **Handle errors appropriately** using domain exceptions and HTTP exceptions.
13. **Consider security** and apply authorization policies where appropriate.
14. **Think about the full stack**: Consider both backend and frontend implications.
15. **Avoid breaking changes**: Maintain backward compatibility in APIs when possible.
16. **Use TypeScript strictly** in the frontend for type safety.

When making changes:
- Search for similar existing patterns before implementing new ones.
- Check for existing specifications, commands, or queries that might be reusable.
- Consider the impact on both the API and the frontend.
- Update tests to cover your changes.
- Ensure migrations or database changes are included if needed.

When in doubt:
- Look for similar examples in the codebase.
- Consult the ASM library documentation: https://github.com/AndrewMcLachlan/ASM
- Consult the MooApp library documentation: https://github.com/AndrewMcLachlan/MooApp
- Ask clarifying questions before making assumptions.

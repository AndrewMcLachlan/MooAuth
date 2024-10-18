using System.Reflection;
using Asm.AspNetCore.Modules;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Asm.MooAuth.Modules.Connectors;

public class Module : IModule
{
    private static readonly Assembly Assembly = typeof(Module).Assembly;

    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddCommandHandlers(Assembly);
        services.AddQueryHandlers(Assembly);

        services.AddScoped<IValidator<Commands.Entra.Create>, Validators.Entra.Create>();
        services.AddScoped<IValidator<Commands.Entra.Update>, Validators.Entra.Update>();

        //services.AddScoped<IValidator<Commands.Auth0.Create>, Validators.Auth0.Create>();
        //services.AddScoped<IValidator<Commands.Auth0.Update>, Validators.Auth0.Update>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        new Endpoints.Connectors().MapGroup(endpoints);
        new Endpoints.Entra().MapGroup(endpoints);
        //new Endpoints.Auth0().MapGroup(endpoints);

        return endpoints;
    }
}

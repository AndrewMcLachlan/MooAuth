using System.Reflection;
using Asm.AspNetCore.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Asm.MooAuth.Modules.Permissions;

public class Module : IModule
{
    private static readonly Assembly Assembly = typeof(System.Reflection.Module).Assembly;

    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddCommandHandlers(Assembly);
        services.AddQueryHandlers(Assembly);

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}

using System.Reflection;
using Asm.AspNetCore.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Asm.MooAuth.Modules.Applications;

public class Module : IModule
{
    private static readonly Assembly Assembly = typeof(Module).Assembly;

    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddCommandHandlers(Assembly);
        services.AddQueryHandlers(Assembly);

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        new Endpoints.Applications().MapGroup(endpoints);
        new Endpoints.Permissions().MapGroup(endpoints);

        return endpoints;
    }
}

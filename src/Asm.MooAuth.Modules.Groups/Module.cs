using System.Reflection;
using Asm.AspNetCore.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Asm.MooAuth.Modules.Groups;

public class Module : IModule
{
    private static readonly Assembly Assembly = typeof(Module).Assembly;

    public IServiceCollection AddServices(IServiceCollection services) =>
        services.AddCommandHandlers(Assembly)
                .AddQueryHandlers(Assembly);

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        new Endpoints.ConnectorGroups().MapGroup(endpoints);
        return endpoints;
    }
}

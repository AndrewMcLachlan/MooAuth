using System.Reflection;
using Asm.AspNetCore.Modules;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Asm.MooAuth.Modules.DataSources;

public class Module : IModule
{
    private static readonly Assembly Assembly = typeof(Module).Assembly;

    public IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddCommandHandlers(Assembly);
        services.AddQueryHandlers(Assembly);

        services.AddScoped<IValidator<Commands.FreeText.Create>, Validators.FreeText.Create>();
        services.AddScoped<IValidator<Commands.FreeText.Update>, Validators.FreeText.Update>();
        services.AddScoped<IValidator<Commands.StaticList.Create>, Validators.StaticList.Create>();
        services.AddScoped<IValidator<Commands.StaticList.Update>, Validators.StaticList.Update>();
        services.AddScoped<IValidator<Commands.ApiList.Create>, Validators.ApiList.Create>();
        services.AddScoped<IValidator<Commands.ApiList.Update>, Validators.ApiList.Update>();

        services.AddHttpClient<IDataSourceApiClient, DataSourceApiClient>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        new Endpoints.DataSources().MapGroup(endpoints);
        new Endpoints.FreeText().MapGroup(endpoints);
        new Endpoints.StaticList().MapGroup(endpoints);
        new Endpoints.ApiList().MapGroup(endpoints);
        new Endpoints.Values().MapGroup(endpoints);

        return endpoints;
    }
}

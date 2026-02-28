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
        services.AddScoped<IValidator<Commands.Checkbox.Create>, Validators.Checkbox.Create>();
        services.AddScoped<IValidator<Commands.Checkbox.Update>, Validators.Checkbox.Update>();
        services.AddScoped<IValidator<Commands.PickList.Create>, Validators.PickList.Create>();
        services.AddScoped<IValidator<Commands.PickList.Update>, Validators.PickList.Update>();
        services.AddScoped<IValidator<Commands.ApiPickList.Create>, Validators.ApiPickList.Create>();
        services.AddScoped<IValidator<Commands.ApiPickList.Update>, Validators.ApiPickList.Update>();

        services.AddHttpClient<IDataSourceApiClient, DataSourceApiClient>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        new Endpoints.DataSources().MapGroup(endpoints);
        new Endpoints.FreeText().MapGroup(endpoints);
        new Endpoints.Checkbox().MapGroup(endpoints);
        new Endpoints.PickList().MapGroup(endpoints);
        new Endpoints.ApiPickList().MapGroup(endpoints);
        new Endpoints.Values().MapGroup(endpoints);

        return endpoints;
    }
}

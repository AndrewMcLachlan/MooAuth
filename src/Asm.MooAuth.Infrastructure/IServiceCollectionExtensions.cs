using Asm.MooAuth.Domain.Entities.Applications;
using Asm.MooAuth.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Asm.MooAuth.Infrastructure.Repositories;
using Asm.MooAuth.Domain.Entities.Permissions;
using Asm.MooAuth.Domain.Entities.Roles;
using Asm.MooAuth.Domain.Entities.Connectors;
using Asm.MooAuth.Domain.Entities.DataSources;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMooAuthDbContext(this IServiceCollection services, IHostEnvironment env, IConfiguration configuration)
    {
        services.AddDbContext<MooAuthContext>((services, options) =>
        {
            options.UseAzureSql(configuration.GetConnectionString("MooAuth"), options =>
            {
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            if (env.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });

        services.AddReadOnlyDbContext<IReadOnlyDbContext, MooAuthContext>((services, options) => options.UseAzureSql(configuration.GetConnectionString("MooAuth"), options =>
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }));

        services.AddDomainEvents(typeof(IApplicationRepository).Assembly);

        return services.AddUnitOfWork<MooAuthContext>();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IApplicationRepository, ApplicationRepository>()
                .AddScoped<IConnectorRepository, ConnectorRepository>()
                .AddScoped<IDataSourceRepository, DataSourceRepository>()
                .AddScoped<IPermissionRepository, PermissionRepository>()
                .AddScoped<IRoleRepository, RoleRepository>();

    public static IServiceCollection AddEntities(this IServiceCollection services) =>
        services.AddAggregateRoots<MooAuthContext>(typeof(IApplicationRepository).Assembly);
}

using Asm.MooAuth;
using Asm.MooAuth.Secrets.Database;

namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseSecretManagerExtensions
{
    /// <summary>
    /// Adds the database secret manager using ASP.NET Core Data Protection for encryption.
    /// </summary>
    /// <remarks>
    /// Data Protection keys are stored locally in the user profile directory (%LOCALAPPDATA%\ASP.NET\DataProtection-Keys).
    /// This is suitable for development/testing. For production, configure Data Protection key storage appropriately.
    /// </remarks>
    public static IServiceCollection AddDatabaseSecretManager(this IServiceCollection services)
    {
        services.AddDataProtection();
        services.AddScoped<ISecretManager, DatabaseSecretManager>();

        return services;
    }
}

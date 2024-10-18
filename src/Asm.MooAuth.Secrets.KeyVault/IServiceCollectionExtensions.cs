using Asm.MooAuth;
using Asm.MooAuth.Secrets.KeyVault;
using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddKeyVaultSecretManager(this WebApplicationBuilder builder, Uri keyVaultUri)
    {
        var services = builder.Services;

        TokenCredential tokenCredential = builder.Environment.IsDevelopment() ? new DefaultAzureCredential(new DefaultAzureCredentialOptions()
        {
            ExcludeManagedIdentityCredential = true,
            ExcludeWorkloadIdentityCredential = true
        }) :
        new ManagedIdentityCredential();

        services.AddSingleton<ISecretManager, KeyVaultSecretManager>();
        services.AddAzureClients(builder =>
        {
            builder.UseCredential(tokenCredential);
            builder.AddSecretClient(keyVaultUri);
        });
        return services;
    }
}

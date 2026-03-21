using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;

namespace Asm.MooAuth.Secrets.KeyVault;

public class KeyVaultSecretManager(SecretClient secretClient, ILogger<KeyVaultSecretManager> logger) : ISecretManager
{
    public Task DeleteSecretAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public async Task<string> GetSecretAsync(string key, CancellationToken cancellationToken = default)
    {
        var secret = await secretClient.GetSecretAsync(key, cancellationToken: cancellationToken);

        if (secret.GetRawResponse().IsError)
        {
            var response = secret.GetRawResponse();
            var content = response.Content.ToString();
            logger.LogError("Failed to retrieve secret {Key} - Code {Status}. Content:\n{Content}", key, response.Status, content);
            throw new Exception("Failed to retrieve secret");
        }

        return secret.Value.Value;
    }

    public async Task SetSecretAsync(string key, string secret, CancellationToken cancellationToken = default)
    {
        var result = await secretClient.SetSecretAsync(key, secret, cancellationToken);

        if (result.GetRawResponse().IsError)
        {

        }
    }
}

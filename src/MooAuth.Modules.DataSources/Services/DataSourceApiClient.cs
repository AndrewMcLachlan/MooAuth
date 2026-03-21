using System.Net.Http.Headers;
using System.Text.Json;
using Asm.MooAuth.Modules.DataSources.Models;
using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;
using Microsoft.Extensions.Caching.Memory;

namespace Asm.MooAuth.Modules.DataSources;

internal class DataSourceApiClient(
    HttpClient httpClient,
    ISecretManager secretManager,
    IMemoryCache cache) : IDataSourceApiClient
{
    public async Task<IEnumerable<DataSourceValue>> FetchValuesAsync(
        Domain.Entities.DataSources.DataSource dataSource,
        CancellationToken cancellationToken)
    {
        var config = dataSource.GetConfig<ApiPickListConfig>();
        if (config == null) return [];

        var cacheKey = $"datasource-values-{dataSource.Key}";
        if (cache.TryGetValue(cacheKey, out IEnumerable<DataSourceValue>? cached) && cached != null)
        {
            return cached;
        }

        await ConfigureAuthenticationAsync(dataSource.Key, config, cancellationToken);

        var response = await httpClient.GetAsync(config.Endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var values = ParseResponse(content, config).ToList();

        cache.Set(cacheKey, values, TimeSpan.FromMinutes(config.CacheMinutes));

        return values;
    }

    private async Task ConfigureAuthenticationAsync(string key, ApiPickListConfig config, CancellationToken cancellationToken)
    {
        switch (config.AuthType)
        {
            case ApiAuthType.ApiKey:
                var apiKey = await secretManager.GetSecretAsync($"datasource-{key}-secret", cancellationToken);
                httpClient.DefaultRequestHeaders.Remove(config.ApiKeyHeader ?? "X-API-Key");
                httpClient.DefaultRequestHeaders.Add(config.ApiKeyHeader ?? "X-API-Key", apiKey);
                break;

            case ApiAuthType.OAuthClientCredentials:
                var token = await GetOAuthTokenAsync(key, config, cancellationToken);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                break;
        }
    }

    private async Task<string> GetOAuthTokenAsync(string key, ApiPickListConfig config, CancellationToken cancellationToken)
    {
        var clientSecret = await secretManager.GetSecretAsync($"datasource-{key}-secret", cancellationToken);

        using var tokenClient = new HttpClient();
        var tokenRequest = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = config.ClientId!,
            ["client_secret"] = clientSecret,
            ["scope"] = config.Scope ?? ""
        });

        var tokenResponse = await tokenClient.PostAsync(config.TokenEndpoint, tokenRequest, cancellationToken);
        tokenResponse.EnsureSuccessStatusCode();

        var tokenJson = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(tokenJson);
        return doc.RootElement.GetProperty("access_token").GetString()!;
    }

    private static IEnumerable<DataSourceValue> ParseResponse(string content, ApiPickListConfig config)
    {
        using var doc = JsonDocument.Parse(content);
        var root = doc.RootElement;

        // Simple array parsing - can be extended for JSONPath support
        if (root.ValueKind == JsonValueKind.Array)
        {
            var id = 0;
            foreach (var element in root.EnumerateArray())
            {
                var key = GetJsonValue(element, config.KeyPath ?? "id");
                var displayValue = GetJsonValue(element, config.DisplayValuePath ?? "name");

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(displayValue))
                {
                    yield return new DataSourceValue
                    {
                        Id = id++,
                        Key = key,
                        DisplayValue = displayValue,
                    };
                }
            }
        }
    }

    private static string? GetJsonValue(JsonElement element, string path)
    {
        if (element.TryGetProperty(path, out var prop))
        {
            return prop.ValueKind == JsonValueKind.String ? prop.GetString() : prop.ToString();
        }
        return null;
    }
}

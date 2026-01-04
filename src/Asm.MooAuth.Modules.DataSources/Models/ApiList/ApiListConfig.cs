namespace Asm.MooAuth.Modules.DataSources.Models.ApiList;

public record ApiListConfig
{
    public required string Endpoint { get; init; }

    public required ApiAuthType AuthType { get; init; }

    // For OAuth Client Credentials
    public string? TokenEndpoint { get; init; }

    public string? ClientId { get; init; }

    public string? Scope { get; init; }

    // For API Key
    public string? ApiKeyHeader { get; init; }

    // Response mapping
    public string? KeyPath { get; init; }

    public string? DisplayValuePath { get; init; }

    // Caching
    public int CacheMinutes { get; init; } = 5;
}

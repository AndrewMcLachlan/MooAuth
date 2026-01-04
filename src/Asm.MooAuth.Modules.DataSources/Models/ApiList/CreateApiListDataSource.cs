namespace Asm.MooAuth.Modules.DataSources.Models.ApiList;

public record CreateApiListDataSource : IDataSourceRequest
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public required ApiListConfig Config { get; init; }

    public string? ClientSecret { get; init; }  // For OAuth

    public string? ApiKey { get; init; }  // For API Key auth
}

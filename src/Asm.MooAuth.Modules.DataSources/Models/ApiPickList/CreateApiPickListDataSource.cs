namespace Asm.MooAuth.Modules.DataSources.Models.ApiPickList;

public record CreateApiPickListDataSource : IDataSourceRequest
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public required ApiPickListConfig Config { get; init; }

    public string? ClientSecret { get; init; }  // For OAuth

    public string? ApiKey { get; init; }  // For API Key auth
}

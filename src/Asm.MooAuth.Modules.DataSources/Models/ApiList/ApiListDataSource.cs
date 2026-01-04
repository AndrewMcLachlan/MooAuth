namespace Asm.MooAuth.Modules.DataSources.Models.ApiList;

public record ApiListDataSource : IDescribed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public ApiListConfig? Config { get; init; }
}

public static class ApiListDataSourceExtensions
{
    public static ApiListDataSource ToApiListModel(this Domain.Entities.DataSources.DataSource dataSource) => new()
    {
        Id = dataSource.Id,
        Name = dataSource.Name,
        Description = dataSource.Description,
        Key = dataSource.Key,
        Config = dataSource.GetConfig<ApiListConfig>(),
    };
}

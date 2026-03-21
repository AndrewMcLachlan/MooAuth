namespace Asm.MooAuth.Modules.DataSources.Models.ApiPickList;

public record ApiPickListDataSource : IDescribed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public ApiPickListConfig? Config { get; init; }
}

public static class ApiPickListDataSourceExtensions
{
    public static ApiPickListDataSource ToApiPickListModel(this Domain.Entities.DataSources.DataSource dataSource) => new()
    {
        Id = dataSource.Id,
        Name = dataSource.Name,
        Description = dataSource.Description,
        Key = dataSource.Key,
        Config = dataSource.GetConfig<ApiPickListConfig>(),
    };
}

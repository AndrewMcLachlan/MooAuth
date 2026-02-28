namespace Asm.MooAuth.Modules.DataSources.Models.PickList;

public record PickListDataSource : IDescribed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public bool AllowMultiple { get; init; }

    public IEnumerable<DataSourceValue> Values { get; init; } = [];
}

public static class PickListDataSourceExtensions
{
    public static PickListDataSource ToPickListModel(this Domain.Entities.DataSources.DataSource dataSource) => new()
    {
        Id = dataSource.Id,
        Name = dataSource.Name,
        Description = dataSource.Description,
        Key = dataSource.Key,
        AllowMultiple = dataSource.GetConfig<PickListConfig>()?.AllowMultiple ?? false,
        Values = dataSource.Values.Select(v => v.ToModel()),
    };
}

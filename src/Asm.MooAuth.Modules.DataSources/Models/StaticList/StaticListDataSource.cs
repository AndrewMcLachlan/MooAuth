namespace Asm.MooAuth.Modules.DataSources.Models.StaticList;

public record StaticListDataSource : IDescribed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public IEnumerable<DataSourceValue> Values { get; init; } = [];
}

public static class StaticListDataSourceExtensions
{
    public static StaticListDataSource ToStaticListModel(this Domain.Entities.DataSources.DataSource dataSource) => new()
    {
        Id = dataSource.Id,
        Name = dataSource.Name,
        Description = dataSource.Description,
        Key = dataSource.Key,
        Values = dataSource.Values.Select(v => v.ToModel()),
    };
}

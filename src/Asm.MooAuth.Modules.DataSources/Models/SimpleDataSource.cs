namespace Asm.MooAuth.Modules.DataSources.Models;

public record SimpleDataSource : INamed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Key { get; init; }

    public required MooAuth.Models.DataSourceType Type { get; init; }
}

public static class SimpleDataSourceExtensions
{
    public static SimpleDataSource ToSimpleModel(this Domain.Entities.DataSources.DataSource dataSource) => new()
    {
        Id = dataSource.Id,
        Name = dataSource.Name,
        Key = dataSource.Key,
        Type = dataSource.DataSourceType,
    };

    public static IQueryable<SimpleDataSource> ToSimpleModel(this IQueryable<Domain.Entities.DataSources.DataSource> dataSources) =>
        dataSources.Select(d => d.ToSimpleModel());
}

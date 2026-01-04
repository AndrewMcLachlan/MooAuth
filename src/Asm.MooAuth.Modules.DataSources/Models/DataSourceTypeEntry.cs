namespace Asm.MooAuth.Modules.DataSources.Models;

public record DataSourceTypeEntry : INamed
{
    public int Id { get; init; }

    public required string Name { get; init; }
}

public static class DataSourceTypeExtensions
{
    public static DataSourceTypeEntry ToModel(this Domain.Entities.DataSources.DataSourceTypeEntry type) => new()
    {
        Id = type.Id,
        Name = type.Name,
    };

    public static IEnumerable<DataSourceTypeEntry> ToModel(this IEnumerable<Domain.Entities.DataSources.DataSourceTypeEntry> types) =>
        types.Select(t => t.ToModel());
}

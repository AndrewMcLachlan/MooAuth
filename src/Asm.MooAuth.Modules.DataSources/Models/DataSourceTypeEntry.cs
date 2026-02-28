using Asm.MooAuth.Models;

namespace Asm.MooAuth.Modules.DataSources.Models;

public record DataSourceTypeEntry : INamed
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public required string DisplayName { get; init; }
}

public static class DataSourceTypeEntryExtensions
{
    public static DataSourceTypeEntry ToModel(this Domain.Entities.DataSources.DataSourceTypeEntry type) => new()
    {
        Id = type.Id,
        Name = type.Name,
        DisplayName = ((DataSourceType)type.Id).ToDisplayName(),
    };

    public static IEnumerable<DataSourceTypeEntry> ToModel(this IEnumerable<Domain.Entities.DataSources.DataSourceTypeEntry> types) =>
        types.Select(t => t.ToModel());
}

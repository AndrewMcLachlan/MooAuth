namespace Asm.MooAuth.Modules.DataSources.Models;

public record DataSourceValue
{
    public required int Id { get; init; }

    public required string Key { get; init; }

    public required string DisplayValue { get; init; }

    public int SortOrder { get; init; }
}

public static class DataSourceValueExtensions
{
    public static DataSourceValue ToModel(this Domain.Entities.DataSources.DataSourceValue value) => new()
    {
        Id = value.Id,
        Key = value.Key,
        DisplayValue = value.DisplayValue,
        SortOrder = value.SortOrder,
    };

    public static IEnumerable<DataSourceValue> ToModel(this IEnumerable<Domain.Entities.DataSources.DataSourceValue> values) =>
        values.Select(v => v.ToModel());
}

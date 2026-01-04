namespace Asm.MooAuth.Modules.DataSources.Models;

public record CreateDataSourceValue
{
    public required string Key { get; init; }

    public required string DisplayValue { get; init; }

    public int SortOrder { get; init; }
}

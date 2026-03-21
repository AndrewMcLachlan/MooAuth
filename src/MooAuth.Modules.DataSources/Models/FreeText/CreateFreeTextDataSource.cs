namespace Asm.MooAuth.Modules.DataSources.Models.FreeText;

public record CreateFreeTextDataSource : IDataSourceRequest
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }
}

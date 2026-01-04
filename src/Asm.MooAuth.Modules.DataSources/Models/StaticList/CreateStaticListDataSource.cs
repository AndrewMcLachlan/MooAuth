namespace Asm.MooAuth.Modules.DataSources.Models.StaticList;

public record CreateStaticListDataSource : IDataSourceRequest
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }

    public IEnumerable<CreateDataSourceValue> Values { get; init; } = [];
}

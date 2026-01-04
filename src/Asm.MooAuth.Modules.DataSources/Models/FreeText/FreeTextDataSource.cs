namespace Asm.MooAuth.Modules.DataSources.Models.FreeText;

public record FreeTextDataSource : IDescribed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public required string Key { get; init; }
}

public static class FreeTextDataSourceExtensions
{
    public static FreeTextDataSource ToFreeTextModel(this Domain.Entities.DataSources.DataSource dataSource) => new()
    {
        Id = dataSource.Id,
        Name = dataSource.Name,
        Description = dataSource.Description,
        Key = dataSource.Key,
    };
}

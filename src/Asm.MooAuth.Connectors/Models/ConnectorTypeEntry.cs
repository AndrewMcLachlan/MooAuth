namespace Asm.MooAuth.Modules.Connectors.Models;
public record ConnectorTypeEntry : INamed
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public Uri? LogoUrl { get; init; }
}

public static class ConnectorTypeExtensions
{
    public static ConnectorTypeEntry ToModel(this Domain.Entities.Connectors.ConnectorType connectorType) => new()
    {
        Id = connectorType.Id,
        Name = connectorType.Name,
        LogoUrl = connectorType.LogoUrl,
    };

    public static IEnumerable<ConnectorTypeEntry> ToModel(this IEnumerable<Domain.Entities.Connectors.ConnectorType> connectorTypes) =>
        connectorTypes.Select(a => a.ToModel());
}

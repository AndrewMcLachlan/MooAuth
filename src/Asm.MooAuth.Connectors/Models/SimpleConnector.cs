namespace Asm.MooAuth.Modules.Connectors.Models;

public record SimpleConnector : INamed
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required MooAuth.Models.ConnectorType Type { get; init; }

    public required string ClientId { get; init; }
}

public static class SimpleConnectorExtensions
{
    public static SimpleConnector ToSimpleModel(this Domain.Entities.Connectors.Connector connector) => new()
    {
        Id = connector.Id,
        Name = connector.Name,
        Type = connector.ConnectorType,
        ClientId = connector.ClientId,
    };

    public static IQueryable<SimpleConnector> ToSimpleModel(this IQueryable<Domain.Entities.Connectors.Connector> connectors) =>
        connectors.Select(a => a.ToSimpleModel());
}

using System.Text.Json;

namespace Asm.MooAuth.Modules.Connectors.Models.Entra;

public record EntraConnector : Connector<EntraConfig>;


public static class EntraConnectorExtensions
{
    public static EntraConnector ToEntraModel(this Domain.Entities.Connectors.Connector connector) => new()
    {
        Id = connector.Id,
        Name = connector.Name,
        Slug = connector.Slug,
        ClientId = connector.ClientId,
        Config = JsonSerializer.Deserialize<EntraConfig>(connector.Config) ?? throw new InvalidOperationException("Failed to deserialize connector config"),
    };
}

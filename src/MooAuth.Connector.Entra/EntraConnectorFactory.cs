using System.Text.Json;
using Azure.Identity;
using Microsoft.Graph;

namespace Asm.MooAuth.Connector.Entra;

public class EntraConnectorFactory(
    IQueryable<Domain.Entities.Connectors.Connector> connectors,
    ISecretManager secretManager) : IConnectorFactory
{
    private static readonly string[] GraphScopes = ["https://graph.microsoft.com/.default"];

    public async Task<IConnector> CreateAsync(CancellationToken cancellationToken = default)
    {
        var connector = await connectors
            .Where(c => c.ConnectorType == Models.ConnectorType.Entra)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("No Entra connector configured");

        var config = JsonSerializer.Deserialize<EntraConfig>(connector.Config)
            ?? throw new InvalidOperationException("Invalid Entra connector configuration");

        var clientSecret = await secretManager.GetSecretAsync(
            $"{connector.Slug}-{connector.ClientId}",
            cancellationToken);

        var credential = new ClientSecretCredential(
            config.TenantId.ToString(),
            connector.ClientId,
            clientSecret);

        var graphClient = new GraphServiceClient(credential, GraphScopes);

        return new EntraConnector(graphClient);
    }
}

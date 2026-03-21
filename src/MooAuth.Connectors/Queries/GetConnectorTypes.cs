using Asm.MooAuth.Modules.Connectors.Models;

namespace Asm.MooAuth.Modules.Connectors.Queries;

public record class GetConnectorTypes : IQuery<IEnumerable<ConnectorTypeEntry>>;

internal class GetConnectorTypesHandler(IQueryable<Domain.Entities.Connectors.ConnectorType> connectorTypes) : IQueryHandler<GetConnectorTypes, IEnumerable<ConnectorTypeEntry>>
{
    public async ValueTask<IEnumerable<ConnectorTypeEntry>> Handle(GetConnectorTypes query, CancellationToken cancellationToken)
    {
        var types = await connectorTypes.ToListAsync(cancellationToken);

        return types.ToModel();
    }
}

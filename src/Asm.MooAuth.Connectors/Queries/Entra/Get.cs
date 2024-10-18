using Asm.MooAuth.Modules.Connectors.Models.Entra;

namespace Asm.MooAuth.Modules.Connectors.Queries.Entra;

public record Get(int Id) : IQuery<EntraConnector>;

internal class GetHandler(IQueryable<Domain.Entities.Connectors.Connector> connectors) : IQueryHandler<Get, EntraConnector>
{
    public async ValueTask<EntraConnector> Handle(Get query, CancellationToken cancellationToken)
    {
        var connector = await connectors
            .Where(a => a.Id == query.Id)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();

        return connector.ToEntraModel();
    }
}

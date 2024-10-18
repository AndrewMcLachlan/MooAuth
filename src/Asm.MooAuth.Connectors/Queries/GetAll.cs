using Asm.MooAuth.Modules.Connectors.Models;

namespace Asm.MooAuth.Modules.Connectors.Queries;

public record GetAll() : IQuery<IEnumerable<SimpleConnector>>;

internal class GetAllHandler(IQueryable<Domain.Entities.Connectors.Connector> connectors) : IQueryHandler<GetAll, IEnumerable<SimpleConnector>>
{
    public async ValueTask<IEnumerable<SimpleConnector>> Handle(GetAll query, CancellationToken cancellationToken) =>
        await connectors.ToSimpleModel().ToListAsync(cancellationToken);
}

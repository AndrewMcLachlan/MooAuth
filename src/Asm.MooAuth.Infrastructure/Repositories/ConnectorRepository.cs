using Asm.MooAuth.Domain.Entities.Connectors;

namespace Asm.MooAuth.Infrastructure.Repositories;
internal class ConnectorRepository(MooAuthContext context) : RepositoryDeleteBase<MooAuthContext, Connector, int>(context), IConnectorRepository
{
    public override void Delete(int id)
    {
        throw new NotImplementedException();
    }
}

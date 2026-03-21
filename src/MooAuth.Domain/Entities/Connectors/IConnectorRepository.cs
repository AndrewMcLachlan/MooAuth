namespace Asm.MooAuth.Domain.Entities.Connectors;
public interface IConnectorRepository : IDeletableRepository<Connector, int>, IWritableRepository<Connector, int>
{
}

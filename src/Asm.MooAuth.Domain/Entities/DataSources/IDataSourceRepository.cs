namespace Asm.MooAuth.Domain.Entities.DataSources;

public interface IDataSourceRepository : IDeletableRepository<DataSource, int>, IWritableRepository<DataSource, int>
{
}

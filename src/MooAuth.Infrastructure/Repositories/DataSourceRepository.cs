using Asm.MooAuth.Domain.Entities.DataSources;

namespace Asm.MooAuth.Infrastructure.Repositories;

internal class DataSourceRepository(MooAuthContext context) : RepositoryDeleteBase<MooAuthContext, DataSource, int>(context), IDataSourceRepository
{
    public override void Delete(int id)
    {
        Entities.Remove(new(id));
    }
}

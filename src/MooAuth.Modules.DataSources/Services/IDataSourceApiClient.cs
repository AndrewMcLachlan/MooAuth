using Asm.MooAuth.Modules.DataSources.Models;

namespace Asm.MooAuth.Modules.DataSources;

public interface IDataSourceApiClient
{
    Task<IEnumerable<DataSourceValue>> FetchValuesAsync(
        Domain.Entities.DataSources.DataSource dataSource,
        CancellationToken cancellationToken);
}

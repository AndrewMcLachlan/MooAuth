using Asm.Domain;
using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Modules.DataSources.Models;
using DataSourceValueModel = Asm.MooAuth.Modules.DataSources.Models.DataSourceValue;

namespace Asm.MooAuth.Modules.DataSources.Queries;

public record GetValues(int DataSourceId) : IQuery<IEnumerable<DataSourceValueModel>>;

internal class GetValuesHandler(
    IQueryable<Domain.Entities.DataSources.DataSource> dataSources,
    IDataSourceApiClient apiClient) : IQueryHandler<GetValues, IEnumerable<DataSourceValueModel>>
{
    public async ValueTask<IEnumerable<DataSourceValueModel>> Handle(GetValues query, CancellationToken cancellationToken)
    {
        var dataSource = await dataSources
            .Specify(new IncludeValuesSpecification())
            .SingleOrDefaultAsync(d => d.Id == query.DataSourceId, cancellationToken)
            ?? throw new NotFoundException();

        return dataSource.DataSourceType switch
        {
            MooAuth.Models.DataSourceType.FreeText => [],
            MooAuth.Models.DataSourceType.Checkbox => [],
            MooAuth.Models.DataSourceType.PickList => dataSource.Values.Select(v => v.ToModel()),
            MooAuth.Models.DataSourceType.ApiPickList => await apiClient.FetchValuesAsync(dataSource, cancellationToken),
            _ => throw new InvalidOperationException("Unknown data source type")
        };
    }
}

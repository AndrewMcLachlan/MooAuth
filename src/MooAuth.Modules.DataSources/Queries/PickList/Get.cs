using Asm.Domain;
using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Modules.DataSources.Models.PickList;

namespace Asm.MooAuth.Modules.DataSources.Queries.PickList;

public record Get(int Id) : IQuery<PickListDataSource>;

internal class GetHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<Get, PickListDataSource>
{
    public async ValueTask<PickListDataSource> Handle(Get query, CancellationToken cancellationToken) =>
        await dataSources
            .Specify(new IncludeValuesSpecification())
            .Where(d => d.Id == query.Id && d.DataSourceType == MooAuth.Models.DataSourceType.PickList)
            .Select(d => d.ToPickListModel())
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();
}

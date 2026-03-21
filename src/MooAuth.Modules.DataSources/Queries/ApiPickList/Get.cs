using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;

namespace Asm.MooAuth.Modules.DataSources.Queries.ApiPickList;

public record Get(int Id) : IQuery<ApiPickListDataSource>;

internal class GetHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<Get, ApiPickListDataSource>
{
    public async ValueTask<ApiPickListDataSource> Handle(Get query, CancellationToken cancellationToken) =>
        await dataSources
            .Where(d => d.Id == query.Id && d.DataSourceType == MooAuth.Models.DataSourceType.ApiPickList)
            .Select(d => d.ToApiPickListModel())
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();
}

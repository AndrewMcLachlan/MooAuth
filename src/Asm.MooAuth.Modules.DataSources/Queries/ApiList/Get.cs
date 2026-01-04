using Asm.MooAuth.Modules.DataSources.Models.ApiList;

namespace Asm.MooAuth.Modules.DataSources.Queries.ApiList;

public record Get(int Id) : IQuery<ApiListDataSource>;

internal class GetHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<Get, ApiListDataSource>
{
    public async ValueTask<ApiListDataSource> Handle(Get query, CancellationToken cancellationToken) =>
        await dataSources
            .Where(d => d.Id == query.Id && d.DataSourceType == MooAuth.Models.DataSourceType.ApiList)
            .Select(d => d.ToApiListModel())
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();
}

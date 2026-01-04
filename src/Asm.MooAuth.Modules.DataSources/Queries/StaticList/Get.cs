using Asm.Domain;
using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Modules.DataSources.Models;
using Asm.MooAuth.Modules.DataSources.Models.StaticList;

namespace Asm.MooAuth.Modules.DataSources.Queries.StaticList;

public record Get(int Id) : IQuery<StaticListDataSource>;

internal class GetHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<Get, StaticListDataSource>
{
    public async ValueTask<StaticListDataSource> Handle(Get query, CancellationToken cancellationToken) =>
        await dataSources
            .Specify(new IncludeValuesSpecification())
            .Where(d => d.Id == query.Id && d.DataSourceType == MooAuth.Models.DataSourceType.StaticList)
            .Select(d => d.ToStaticListModel())
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();
}

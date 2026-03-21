using Asm.MooAuth.Modules.DataSources.Models;

namespace Asm.MooAuth.Modules.DataSources.Queries;

public record GetAll() : IQuery<IEnumerable<SimpleDataSource>>;

internal class GetAllHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<GetAll, IEnumerable<SimpleDataSource>>
{
    public async ValueTask<IEnumerable<SimpleDataSource>> Handle(GetAll query, CancellationToken cancellationToken) =>
        await dataSources.ToSimpleModel().ToListAsync(cancellationToken);
}

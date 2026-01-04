using Asm.MooAuth.Modules.DataSources.Models;

namespace Asm.MooAuth.Modules.DataSources.Queries;

public record GetDataSourceTypes() : IQuery<IEnumerable<DataSourceTypeEntry>>;

internal class GetDataSourceTypesHandler(IQueryable<Domain.Entities.DataSources.DataSourceTypeEntry> types) : IQueryHandler<GetDataSourceTypes, IEnumerable<DataSourceTypeEntry>>
{
    public async ValueTask<IEnumerable<DataSourceTypeEntry>> Handle(GetDataSourceTypes query, CancellationToken cancellationToken) =>
        await types.Select(t => new DataSourceTypeEntry { Id = t.Id, Name = t.Name }).ToListAsync(cancellationToken);
}

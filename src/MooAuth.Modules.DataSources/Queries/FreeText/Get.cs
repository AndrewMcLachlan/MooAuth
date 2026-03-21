using Asm.MooAuth.Modules.DataSources.Models.FreeText;

namespace Asm.MooAuth.Modules.DataSources.Queries.FreeText;

public record Get(int Id) : IQuery<FreeTextDataSource>;

internal class GetHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<Get, FreeTextDataSource>
{
    public async ValueTask<FreeTextDataSource> Handle(Get query, CancellationToken cancellationToken) =>
        await dataSources
            .Where(d => d.Id == query.Id && d.DataSourceType == MooAuth.Models.DataSourceType.FreeText)
            .Select(d => d.ToFreeTextModel())
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();
}

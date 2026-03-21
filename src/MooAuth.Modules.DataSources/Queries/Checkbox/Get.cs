using Asm.MooAuth.Modules.DataSources.Models.Checkbox;

namespace Asm.MooAuth.Modules.DataSources.Queries.Checkbox;

public record Get(int Id) : IQuery<CheckboxDataSource>;

internal class GetHandler(IQueryable<Domain.Entities.DataSources.DataSource> dataSources) : IQueryHandler<Get, CheckboxDataSource>
{
    public async ValueTask<CheckboxDataSource> Handle(Get query, CancellationToken cancellationToken) =>
        await dataSources
            .Where(d => d.Id == query.Id && d.DataSourceType == MooAuth.Models.DataSourceType.Checkbox)
            .Select(d => d.ToCheckboxModel())
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException();
}

namespace Asm.MooAuth.Domain.Entities.DataSources;

public class IncludeValuesSpecification : ISpecification<DataSource>
{
    public IQueryable<DataSource> Apply(IQueryable<DataSource> query) =>
        query.Include(d => d.Values.OrderBy(v => v.SortOrder));
}

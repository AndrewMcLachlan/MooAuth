namespace Asm.MooAuth.Domain.Entities.Applications;

internal class IncludePermissionsSpecification : ISpecification<Application>
{
    public IQueryable<Application> Apply(IQueryable<Application> query) =>
        query.Include(a => a.Permissions);
}

namespace Asm.MooAuth.Domain.Entities.Actors;

public class IncludeRoleResourcesSpecification : ISpecification<Actor>
{
    public IQueryable<Actor> Apply(IQueryable<Actor> query) =>
        query.Include(a => a.RoleResources).ThenInclude(rr => rr.Role);
}

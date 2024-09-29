namespace Asm.MooAuth.Domain.Entities.Roles;

public class IncludePermissionsSpecification : ISpecification<Role>
{
    public IQueryable<Role> Apply(IQueryable<Role> query) =>
        query.Include(a => a.Permissions).ThenInclude(p => p.Application);
}

namespace Asm.MooAuth.Domain.Entities.Roles;

public interface IRoleRepository : IWritableRepository<Role, int>, IDeletableRepository<Role, int>
{
}

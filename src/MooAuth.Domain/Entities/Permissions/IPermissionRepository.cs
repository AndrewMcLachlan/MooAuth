namespace Asm.MooAuth.Domain.Entities.Permissions;

public interface IPermissionRepository : IWritableRepository<Permission, int>, IDeletableRepository<Permission, int>
{
}

using Asm.MooAuth.Domain.Entities.Permissions;

namespace Asm.MooAuth.Infrastructure.Repositories;
internal class PermissionRepository(MooAuthContext context) : RepositoryDeleteBase<MooAuthContext, Permission, int>(context), IPermissionRepository
{
    public override void Delete(int id)
    {
        throw new NotImplementedException();
    }
}

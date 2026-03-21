using Asm.MooAuth.Domain.Entities.Roles;

namespace Asm.MooAuth.Infrastructure.Repositories;
internal class RoleRepository(MooAuthContext context) : RepositoryDeleteBase<MooAuthContext, Role, int>(context), IRoleRepository
{
    public override void Delete(int id)
    {
        Entities.Remove(new(id));
    }
}

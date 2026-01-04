using Asm.MooAuth.Domain.Entities.Applications;

namespace Asm.MooAuth.Infrastructure.Repositories;

internal class ApplicationRepository(MooAuthContext context) : RepositoryDeleteBase<MooAuthContext, Application, int>(context), IApplicationRepository
{
    public override void Delete(int id)
    {
        Entities.Remove(new(id));
    }
}

using Asm.MooAuth.Domain.Entities.Actors;
using Microsoft.EntityFrameworkCore;

namespace Asm.MooAuth.Infrastructure.Repositories;

internal class ActorRepository(MooAuthContext context) : RepositoryDeleteBase<MooAuthContext, Actor, int>(context), IActorRepository
{
    public override void Delete(int id)
    {
        Entities.Remove(new(id));
    }

    public async Task<Actor?> GetByExternalIdAsync(string externalId, Models.ActorType actorType, CancellationToken cancellationToken = default)
    {
        return await Entities
            .Include(a => a.RoleResources).ThenInclude(rr => rr.Role)
            .Include(a => a.RoleResources).ThenInclude(rr => rr.DataSource)
            .FirstOrDefaultAsync(a => a.ExternalId == externalId && a.ActorType == actorType, cancellationToken);
    }
}

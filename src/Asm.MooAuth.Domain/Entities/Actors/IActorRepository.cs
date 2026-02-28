namespace Asm.MooAuth.Domain.Entities.Actors;

public interface IActorRepository : IDeletableRepository<Actor, int>, IWritableRepository<Actor, int>
{
    Task<Actor?> GetByExternalIdAsync(string externalId, Models.ActorType actorType, CancellationToken cancellationToken = default);
}

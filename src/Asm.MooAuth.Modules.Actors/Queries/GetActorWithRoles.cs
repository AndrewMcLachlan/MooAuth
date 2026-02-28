using Asm.MooAuth.Domain.Entities.Actors;
using Asm.MooAuth.Modules.Actors.Models;
using ActorModel = Asm.MooAuth.Modules.Actors.Models.Actor;

namespace Asm.MooAuth.Modules.Actors.Queries;

public record GetActorWithRoles(MooAuth.Models.ActorType ActorType, string ExternalId) : IQuery<ActorModel?>;

internal class GetActorWithRolesHandler(IActorRepository repository) : IQueryHandler<GetActorWithRoles, ActorModel?>
{
    public async ValueTask<ActorModel?> Handle(GetActorWithRoles query, CancellationToken cancellationToken)
    {
        var actor = await repository.GetByExternalIdAsync(query.ExternalId, query.ActorType, cancellationToken);

        return actor?.ToModel();
    }
}

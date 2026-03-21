using Asm.Domain;
using Asm.MooAuth.Domain.Entities.Actors;
using Asm.MooAuth.Modules.Actors.Models;
using ActorModel = Asm.MooAuth.Modules.Actors.Models.Actor;

namespace Asm.MooAuth.Modules.Actors.Commands;

public record RemoveRoleAssignment(
    MooAuth.Models.ActorType ActorType,
    string ExternalId,
    int RoleId) : ICommand<ActorModel?>;

internal class RemoveRoleAssignmentHandler(IUnitOfWork unitOfWork, IActorRepository repository) : ICommandHandler<RemoveRoleAssignment, ActorModel?>
{
    public async ValueTask<ActorModel?> Handle(RemoveRoleAssignment command, CancellationToken cancellationToken)
    {
        var actor = await repository.GetByExternalIdAsync(command.ExternalId, command.ActorType, cancellationToken);

        if (actor == null)
        {
            return null;
        }

        var roleResourcesToRemove = actor.RoleResources.Where(rr => rr.RoleId == command.RoleId).ToList();

        foreach (var roleResource in roleResourcesToRemove)
        {
            actor.RoleResources.Remove(roleResource);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Reload to get updated state
        actor = await repository.GetByExternalIdAsync(command.ExternalId, command.ActorType, cancellationToken);

        return actor?.ToModel();
    }
}

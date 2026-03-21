using Asm.Domain;
using Asm.MooAuth.Domain.Entities.Actors;
using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Modules.Actors.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActorModel = Asm.MooAuth.Modules.Actors.Models.Actor;

namespace Asm.MooAuth.Modules.Actors.Commands;

public record AddRoleAssignment(
    MooAuth.Models.ActorType ActorType,
    string ExternalId,
    [FromBody] CreateActorRoleAssignment Assignment) : ICommand<ActorModel>;

internal class AddRoleAssignmentHandler(
    IUnitOfWork unitOfWork,
    IActorRepository repository,
    IQueryable<DataSource> dataSources) : ICommandHandler<AddRoleAssignment, ActorModel>
{
    public async ValueTask<ActorModel> Handle(AddRoleAssignment command, CancellationToken cancellationToken)
    {
        var actor = await repository.GetByExternalIdAsync(command.ExternalId, command.ActorType, cancellationToken);

        if (actor == null)
        {
            actor = new Domain.Entities.Actors.Actor
            {
                ExternalId = command.ExternalId,
                ActorType = command.ActorType,
                ConnectorId = command.Assignment.ConnectorId,
            };
            repository.Add(actor);
        }

        // If no resources provided, add global assignment (both null)
        if (!command.Assignment.Resources.Any())
        {
            if (!actor.RoleResources.Any(rr => rr.RoleId == command.Assignment.RoleId && rr.DataSourceId == null && rr.ResourceValue == null))
            {
                actor.RoleResources.Add(new Domain.Entities.Actors.ActorRoleResource
                {
                    RoleId = command.Assignment.RoleId,
                    DataSourceId = null,
                    ResourceValue = null,
                });
            }
        }
        else
        {
            foreach (var resource in command.Assignment.Resources)
            {
                // Validate data source exists if specified
                if (resource.DataSourceId.HasValue)
                {
                    var exists = await dataSources.AnyAsync(
                        ds => ds.Id == resource.DataSourceId.Value,
                        cancellationToken);
                    if (!exists)
                    {
                        throw new BadHttpRequestException(
                            $"Data source {resource.DataSourceId} not found");
                    }
                }

                // Check for duplicates
                if (!actor.RoleResources.Any(rr =>
                    rr.RoleId == command.Assignment.RoleId &&
                    rr.DataSourceId == resource.DataSourceId &&
                    rr.ResourceValue == resource.Value))
                {
                    actor.RoleResources.Add(new Domain.Entities.Actors.ActorRoleResource
                    {
                        RoleId = command.Assignment.RoleId,
                        DataSourceId = resource.DataSourceId,
                        ResourceValue = resource.Value,
                    });
                }
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Reload to get role names and data source info
        actor = await repository.GetByExternalIdAsync(command.ExternalId, command.ActorType, cancellationToken);

        return actor!.ToModel();
    }
}

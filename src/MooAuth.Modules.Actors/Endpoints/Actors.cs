using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.Actors.Commands;
using Asm.MooAuth.Modules.Actors.Models;
using Asm.MooAuth.Modules.Actors.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ActorModel = Asm.MooAuth.Modules.Actors.Models.Actor;

namespace Asm.MooAuth.Modules.Actors.Endpoints;

internal class Actors : EndpointGroupBase
{
    public override string Name => "Actors";

    public override string Path => "/actors";

    public override string Tags => "Actors";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<GetActorWithRoles, ActorModel?>("/{actorType}/{externalId}")
               .WithNames("Get Actor With Roles");

        builder.MapCommand<AddRoleAssignment, ActorModel>("/{actorType}/{externalId}/roles")
               .WithNames("Add Role Assignment");

        builder.MapDelete<RemoveRoleAssignment, ActorModel?>("/{actorType}/{externalId}/roles/{roleId}")
               .WithNames("Remove Role Assignment")
               .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

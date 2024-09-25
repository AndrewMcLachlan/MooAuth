using Asm.AspNetCore;
using Asm.MooAuth.Modules.Roles.Commands.Roles;
using Asm.MooAuth.Modules.Roles.Models;
using Asm.MooAuth.Modules.Roles.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Roles.Endpoints;

internal class Roles : EndpointGroupBase
{
    public override string Name => "Roles";

    public override string Path => "/roles";

    public override string Tags => "Roles";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<GetAll, IEnumerable<Role>>("/")
            .WithNames("Get All Roles");

        builder.MapQuery<Get, Role>("/{id}")
            .WithNames("Get Role")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, Role>("/", "Get Role".ToMachine(), a => new { a.Id }, CommandBinding.Body)
            .WithNames("Create Role");

        builder.MapPatchCommand<Update, Role>("/{id}")
            .WithNames("Update Role")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPutCommand<Update, Role>("/{roleId}/permissions/{id}")
            .WithNames("Add Permission")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Delete>("/{roleId}/permissions/{id}")
            .WithNames("Remove Role")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Delete>("/{id}")
            .WithNames("Delete Role")
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

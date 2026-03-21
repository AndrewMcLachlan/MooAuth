using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
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
            .WithNames("Get All Roles")
            .ProducesProblem(StatusCodes.Status403Forbidden);

        builder.MapQuery<Get, Role>("/{id}")
            .WithNames("Get Role")
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, Role>("/", "Get Role".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Role")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, Role>("/{id}")
            .WithNames("Update Role")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPutCommand<AddPermission>("/{roleId}/permissions/{permissionId}")
            .WithNames("Add Permission")
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<RemovePermission>("/{roleId}/permissions/{permissionId}")
            .WithNames("Remove Role")
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Delete>("/{id}")
            .WithNames("Delete Role")
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

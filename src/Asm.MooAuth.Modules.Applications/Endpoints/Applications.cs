using Asm.AspNetCore;
using Asm.MooAuth.Modules.Applications.Commands.Applications;
using Asm.MooAuth.Modules.Applications.Models;
using Asm.MooAuth.Modules.Applications.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Applications.Endpoints;
internal class Applications : EndpointGroupBase
{
    public override string Name => "Applications";

    public override string Path => "/applications";

    public override string Tags => "Applications";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<GetAll, IEnumerable<Application>>("/")
            .WithNames("Get All Applications");

        builder.MapQuery<Get, Application>("/{id}")
            .WithNames("Get Application")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, Application>("/", "Get Application".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Application");

        builder.MapPatchCommand<Update, Application>("/{id}", CommandBinding.Parameters)
            .WithNames("Update Application")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Delete>("/{id}")
            .WithNames("Delete Application")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapQuery<GetList, IEnumerable<SimpleApplication>>("/permissions")
            .WithNames("Get Permission List");

        builder.MapQuery<Queries.Permissions.Get, Permission>("/{applicationId}/permissions/{id}")
            .WithNames("Get Permission")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Commands.Permissions.Create, Permission>("/{applicationId}/permissions", "Get Permission".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Permission");

        builder.MapPatchCommand<Commands.Permissions.Update, Permission>("/{applicationId}/permissions/{id}")
            .WithNames("Update Permission")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Commands.Permissions.Delete>("/{applicationId}/permissions/{id}")
            .WithNames("Delete Permission")
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

/*using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.Connectors.Commands.Auth0;
using Asm.MooAuth.Modules.Connectors.Models;
using Asm.MooAuth.Modules.Connectors.Queries;
using Asm.MooAuth.Modules.Connectors.Queries.Entra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Connectors.Endpoints;
internal class Connectors : EndpointGroupBase
{
    public override string Name => "Connectors";

    public override string Path => "/connectors/Auth";

    public override string Tags => "Connectors";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, Auth0Connector>("/{id}")
            .WithNames("Get Connector")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, Connector>("/", "Get Connector".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Connector")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, Connector>("/{id}", CommandBinding.Parameters)
            .WithNames("Update Connector")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Delete>("/{id}")
            .WithNames("Delete Connector")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapQuery<GetList, IEnumerable<SimpleConnector>>("/permissions")
            .WithNames("Get Permission List");

        builder.MapQuery<Queries.Permissions.Get, Permission>("/{connectorId}/permissions/{id}")
            .WithNames("Get Permission")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Commands.Permissions.Create, Permission>("/{connectorId}/permissions", "Get Permission".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Permission")
            .WithValidation<Create>();

        builder.MapPatchCommand<Commands.Permissions.Update, Permission>("/{connectorId}/permissions/{id}")
            .WithNames("Update Permission")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<Commands.Permissions.Delete>("/{connectorId}/permissions/{id}")
            .WithNames("Delete Permission")
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
*/

using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.Connectors.Commands.Entra;
using Asm.MooAuth.Modules.Connectors.Models.Entra;
using Asm.MooAuth.Modules.Connectors.Queries.Entra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Connectors.Endpoints;
internal class Entra : EndpointGroupBase
{
    public override string Name => "Connectors";

    public override string Path => "/connectors/entra";

    public override string Tags => "Connectors";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, EntraConnector>("/{id}")
            .WithNames("Get Entra Connector")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, EntraConnector>("/", "Get Entra Connector".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Entra Connector")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, EntraConnector>("/{id}", CommandBinding.Parameters)
            .WithNames("Update Entra Connector")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

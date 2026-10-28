using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.Connectors.Commands;
using Asm.MooAuth.Modules.Connectors.Models;
using Asm.MooAuth.Modules.Connectors.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Postie.AspNetCore;

namespace Asm.MooAuth.Modules.Connectors.Endpoints;
internal class Connectors : EndpointGroupBase
{
    public override string Path => "/connectors";

    public override string? Tag => "Connectors";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {

        builder.MapQuery<GetAll, IEnumerable<SimpleConnector>>("/")
               .WithNames("Get All Connectors");

        builder.MapQuery<GetConnectorTypes, IEnumerable<ConnectorTypeEntry>>("/available")
               .WithNames("Get Available Connector Types");

        builder.MapDeleteCommand<Delete>("/{id}")
               .WithNames("Delete Connector")
               .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

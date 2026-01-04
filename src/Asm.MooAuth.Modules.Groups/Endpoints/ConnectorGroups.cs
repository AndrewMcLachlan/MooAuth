using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Models;
using Asm.MooAuth.Modules.Groups.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Groups.Endpoints;

public class ConnectorGroups : EndpointGroupBase
{
    public override string Name => "Groups";

    public override string Path => "/groups";

    public override string Tags => "Groups";

    protected override void MapEndpoints(IEndpointRouteBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapQuery<GetConnectorGroups, PagedResult<ConnectorGroup>>("/")
            .WithNames("Get Groups")
            .Produces<PagedResult<ConnectorGroup>>();
    }
}

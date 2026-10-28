using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Models;
using Asm.MooAuth.Modules.Groups.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Postie.AspNetCore;

namespace Asm.MooAuth.Modules.Groups.Endpoints;

public class ConnectorGroups : EndpointGroupBase
{
    public override string Path => "/groups";

    public override string? Tag => "Groups";

    protected override void MapEndpoints(IEndpointRouteBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapQuery<GetConnectorGroups, PagedResult<ConnectorGroup>>("/")
            .WithNames("Get Groups")
            .Produces<PagedResult<ConnectorGroup>>();
    }
}

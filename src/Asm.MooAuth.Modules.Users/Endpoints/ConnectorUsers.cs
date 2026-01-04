using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Models;
using Asm.MooAuth.Modules.Users.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Users.Endpoints;

public class ConnectorUsers : EndpointGroupBase
{
    public override string Name => "Users";

    public override string Path => "/users";

    public override string Tags => "Users";

    protected override void MapEndpoints(IEndpointRouteBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapQuery<GetConnectorUsers, PagedResult<ConnectorUser>>("/")
            .WithNames("Get Users")
            .Produces<PagedResult<ConnectorUser>>();
    }
}

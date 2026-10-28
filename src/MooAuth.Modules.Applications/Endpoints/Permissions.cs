using Asm.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Applications.Endpoints;
internal class Permissions : EndpointGroupBase
{
    public override string Path => "/permissions";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {

    }
}

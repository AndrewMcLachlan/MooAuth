using Asm.AspNetCore;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Applications.Endpoints;
internal class Permissions : EndpointGroupBase
{
    public override string Name => "Permissions";

    public override string Path => "/permissions";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {

    }
}

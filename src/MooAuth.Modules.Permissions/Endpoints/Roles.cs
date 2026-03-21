using Asm.AspNetCore;
using Asm.MooAuth.Modules.Permissions.Models;
using Asm.MooAuth.Modules.Permissions.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.Permissions.Endpoints;
internal class Roles : EndpointGroupBase
{
    public override string Name => "Permissions";

    public override string Path => "/permissions";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<GetAll, IEnumerable<SimplePermission>>("/")
            .WithNames("Get All Permissions")
            .WithSummary("Get all permissions");
    }
}

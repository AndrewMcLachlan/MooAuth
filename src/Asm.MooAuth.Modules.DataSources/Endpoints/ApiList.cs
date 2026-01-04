using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.ApiList;
using Asm.MooAuth.Modules.DataSources.Models.ApiList;
using Asm.MooAuth.Modules.DataSources.Queries.ApiList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class ApiList : EndpointGroupBase
{
    public override string Name => "Data Sources";

    public override string Path => "/datasources/apilist";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, ApiListDataSource>("/{id}")
            .WithNames("Get ApiList Data Source")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, ApiListDataSource>("/", "Get ApiList Data Source".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create ApiList Data Source")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, ApiListDataSource>("/{id}", CommandBinding.Parameters)
            .WithNames("Update ApiList Data Source")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

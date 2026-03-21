using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.ApiPickList;
using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;
using Asm.MooAuth.Modules.DataSources.Queries.ApiPickList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class ApiPickList : EndpointGroupBase
{
    public override string Name => "Data Sources";

    public override string Path => "/datasources/apipicklist";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, ApiPickListDataSource>("/{id}")
            .WithNames("Get ApiPickList Data Source")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, ApiPickListDataSource>("/", "Get ApiPickList Data Source".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create ApiPickList Data Source")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, ApiPickListDataSource>("/{id}", CommandBinding.Parameters)
            .WithNames("Update ApiPickList Data Source")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

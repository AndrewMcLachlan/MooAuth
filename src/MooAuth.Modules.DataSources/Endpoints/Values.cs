using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.Values;
using Asm.MooAuth.Modules.DataSources.Models;
using Asm.MooAuth.Modules.DataSources.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class Values : EndpointGroupBase
{
    public override string Name => "Data Source Values";

    public override string Path => "/datasources/{dataSourceId}/values";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<GetValues, IEnumerable<DataSourceValue>>("/")
            .WithNames("Get Data Source Values")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<AddValue, DataSourceValue>("/", "Get Data Source Values".ToMachine(), _ => new { }, CommandBinding.Parameters)
            .WithNames("Add Data Source Value")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPatchCommand<UpdateValue, DataSourceValue>("/{valueId}", CommandBinding.Parameters)
            .WithNames("Update Data Source Value")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapDelete<RemoveValue>("/{valueId}")
            .WithNames("Remove Data Source Value")
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands;
using Asm.MooAuth.Modules.DataSources.Models;
using Asm.MooAuth.Modules.DataSources.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class DataSources : EndpointGroupBase
{
    public override string Name => "Data Sources";

    public override string Path => "/datasources";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<GetAll, IEnumerable<SimpleDataSource>>("/")
               .WithNames("Get All Data Sources");

        builder.MapQuery<GetDataSourceTypes, IEnumerable<DataSourceTypeEntry>>("/types")
               .WithNames("Get Data Source Types");

        builder.MapDelete<Delete>("/{id}")
               .WithNames("Delete Data Source")
               .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.StaticList;
using Asm.MooAuth.Modules.DataSources.Models.StaticList;
using Asm.MooAuth.Modules.DataSources.Queries.StaticList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class StaticList : EndpointGroupBase
{
    public override string Name => "Data Sources";

    public override string Path => "/datasources/staticlist";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, StaticListDataSource>("/{id}")
            .WithNames("Get StaticList Data Source")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, StaticListDataSource>("/", "Get StaticList Data Source".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create StaticList Data Source")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, StaticListDataSource>("/{id}", CommandBinding.Parameters)
            .WithNames("Update StaticList Data Source")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

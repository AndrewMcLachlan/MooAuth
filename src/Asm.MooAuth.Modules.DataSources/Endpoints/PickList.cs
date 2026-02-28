using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.PickList;
using Asm.MooAuth.Modules.DataSources.Models.PickList;
using Asm.MooAuth.Modules.DataSources.Queries.PickList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class PickList : EndpointGroupBase
{
    public override string Name => "Data Sources";

    public override string Path => "/datasources/picklist";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, PickListDataSource>("/{id}")
            .WithNames("Get PickList Data Source")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, PickListDataSource>("/", "Get PickList Data Source".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create PickList Data Source")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, PickListDataSource>("/{id}", CommandBinding.Parameters)
            .WithNames("Update PickList Data Source")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

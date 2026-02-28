using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.Checkbox;
using Asm.MooAuth.Modules.DataSources.Models.Checkbox;
using Asm.MooAuth.Modules.DataSources.Queries.Checkbox;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class Checkbox : EndpointGroupBase
{
    public override string Name => "Data Sources";

    public override string Path => "/datasources/checkbox";

    public override string Tags => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, CheckboxDataSource>("/{id}")
            .WithNames("Get Checkbox Data Source")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, CheckboxDataSource>("/", "Get Checkbox Data Source".ToMachine(), a => new { a.Id }, CommandBinding.Parameters)
            .WithNames("Create Checkbox Data Source")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, CheckboxDataSource>("/{id}", CommandBinding.Parameters)
            .WithNames("Update Checkbox Data Source")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

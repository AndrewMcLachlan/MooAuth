using Asm.AspNetCore;
using Asm.AspNetCore.Routing;
using Asm.MooAuth.Modules.DataSources.Commands.FreeText;
using Asm.MooAuth.Modules.DataSources.Models.FreeText;
using Asm.MooAuth.Modules.DataSources.Queries.FreeText;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Postie.AspNetCore;

namespace Asm.MooAuth.Modules.DataSources.Endpoints;

internal class FreeText : EndpointGroupBase
{
    public override string Path => "/datasources/freetext";

    public override string? Tag => "Data Sources";

    protected override void MapEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapQuery<Get, FreeTextDataSource>("/{id}")
            .WithNames("Get FreeText Data Source")
            .ProducesProblem(StatusCodes.Status404NotFound);

        builder.MapPostCreate<Create, FreeTextDataSource>("/", "Get FreeText Data Source".ToMachine(), a => new { a.Id }, RequestBinding.Parameters)
            .WithNames("Create FreeText Data Source")
            .WithValidation<Create>();

        builder.MapPatchCommand<Update, FreeTextDataSource>("/{id}", binding: RequestBinding.Parameters)
            .WithNames("Update FreeText Data Source")
            .WithValidation<Update>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}

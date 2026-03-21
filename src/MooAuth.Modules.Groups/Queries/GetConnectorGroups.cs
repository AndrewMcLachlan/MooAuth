using Asm.MooAuth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asm.MooAuth.Modules.Groups.Queries;

public record GetConnectorGroups(
    [FromQuery] int Page = 1,
    [FromQuery] int PageSize = 20,
    [FromQuery] string? Search = null
) : IQuery<PagedResult<ConnectorGroup>>;

internal class GetConnectorGroupsHandler(IConnectorFactory connectorFactory) : IQueryHandler<GetConnectorGroups, PagedResult<ConnectorGroup>>
{
    public async ValueTask<PagedResult<ConnectorGroup>> Handle(GetConnectorGroups query, CancellationToken cancellationToken)
    {
        var connector = await connectorFactory.CreateAsync(cancellationToken);
        return await connector.GetGroupsAsync(query.Page, query.PageSize, query.Search, cancellationToken);
    }
}

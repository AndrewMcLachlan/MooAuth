using Asm.MooAuth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asm.MooAuth.Modules.Users.Queries;

public record GetConnectorUsers(
    [FromQuery] int Page = 1,
    [FromQuery] int PageSize = 20,
    [FromQuery] string? Search = null
) : IQuery<PagedResult<ConnectorUser>>;

internal class GetConnectorUsersHandler(IConnectorFactory connectorFactory) : IQueryHandler<GetConnectorUsers, PagedResult<ConnectorUser>>
{
    public async ValueTask<PagedResult<ConnectorUser>> Handle(GetConnectorUsers query, CancellationToken cancellationToken)
    {
        var connector = await connectorFactory.CreateAsync(cancellationToken);
        return await connector.GetUsersAsync(query.Page, query.PageSize, query.Search, cancellationToken);
    }
}

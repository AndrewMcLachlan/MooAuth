using Asm.MooAuth.Models;
using Microsoft.Graph;

namespace Asm.MooAuth.Connector.Entra;

public class EntraConnector(GraphServiceClient graphClient) : IConnector
{
    public async Task<PagedResult<ConnectorUser>> GetUsersAsync(int page, int pageSize, string? search = null, CancellationToken cancellationToken = default)
    {
        // Note: Microsoft Graph API doesn't support traditional $skip for users.
        // For now, we support search and return the first page of results.
        // Full pagination would require following @odata.nextLink or using iterators.

        var response = await graphClient.Users.GetAsync(config =>
        {
            config.QueryParameters.Top = pageSize;
            config.QueryParameters.Select = ["id", "mail", "userPrincipalName", "displayName", "givenName", "surname"];
            config.QueryParameters.Count = true;
            config.QueryParameters.Orderby = ["displayName"];
            config.Headers.Add("ConsistencyLevel", "eventual");

            if (!String.IsNullOrWhiteSpace(search))
            {
                config.QueryParameters.Search = $"\"displayName:{search}\" OR \"mail:{search}\"";
            }
        }, cancellationToken);

        var users = response?.Value?.Select(u => new ConnectorUser
        {
            Id = u.Id ?? String.Empty,
            Email = u.Mail ?? u.UserPrincipalName ?? String.Empty,
            DisplayName = u.DisplayName ?? String.Empty,
            FirstName = u.GivenName,
            LastName = u.Surname,
        }) ?? [];

        return new PagedResult<ConnectorUser>
        {
            Results = users,
            Total = (int)(response?.OdataCount ?? 0),
        };
    }

    public async Task<PagedResult<ConnectorGroup>> GetGroupsAsync(int page, int pageSize, string? search = null, CancellationToken cancellationToken = default)
    {
        // Note: Same limitation as users - no traditional $skip support.

        var response = await graphClient.Groups.GetAsync(config =>
        {
            config.QueryParameters.Top = pageSize;
            config.QueryParameters.Select = ["id", "displayName", "description"];
            config.QueryParameters.Count = true;
            config.QueryParameters.Orderby = ["displayName"];
            config.Headers.Add("ConsistencyLevel", "eventual");

            if (!String.IsNullOrWhiteSpace(search))
            {
                config.QueryParameters.Search = $"\"displayName:{search}\"";
            }
        }, cancellationToken);

        var groups = response?.Value?.Select(g => new ConnectorGroup
        {
            Id = g.Id ?? String.Empty,
            Name = g.DisplayName ?? String.Empty,
            Description = g.Description,
        }) ?? [];

        return new PagedResult<ConnectorGroup>
        {
            Results = groups,
            Total = (int)(response?.OdataCount ?? 0),
        };
    }
}

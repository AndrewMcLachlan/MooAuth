using Asm.MooAuth.Models;

namespace Asm.MooAuth;

public interface IConnector
{
    Task<PagedResult<ConnectorUser>> GetUsersAsync(int page, int pageSize, string? search = null, CancellationToken cancellationToken = default);

    Task<PagedResult<ConnectorGroup>> GetGroupsAsync(int page, int pageSize, string? search = null, CancellationToken cancellationToken = default);
}

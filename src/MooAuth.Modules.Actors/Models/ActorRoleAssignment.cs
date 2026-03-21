namespace Asm.MooAuth.Modules.Actors.Models;

/// <summary>
/// A resource with its data source context.
/// </summary>
public record ActorRoleResource
{
    public int? DataSourceId { get; init; }

    public string? DataSourceName { get; init; }

    public string? DataSourceKey { get; init; }

    public string? ResourceValue { get; init; }
}

public record ActorRoleAssignment
{
    public required int RoleId { get; init; }

    public required string RoleName { get; init; }

    public IEnumerable<ActorRoleResource> Resources { get; init; } = [];
}

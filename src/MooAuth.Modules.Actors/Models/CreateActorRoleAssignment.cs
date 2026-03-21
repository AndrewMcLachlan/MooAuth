namespace Asm.MooAuth.Modules.Actors.Models;

/// <summary>
/// A single resource entry with its data source context.
/// </summary>
public record ResourceEntry
{
    /// <summary>
    /// The data source ID.
    /// </summary>
    public int? DataSourceId { get; init; }

    /// <summary>
    /// The resource value - can be a DataSourceValue.Key or a free-text string.
    /// </summary>
    public string? Value { get; init; }
}

public record CreateActorRoleAssignment
{
    public required int RoleId { get; init; }

    public required int ConnectorId { get; init; }

    /// <summary>
    /// List of resources to assign. Empty list = global assignment.
    /// </summary>
    public IEnumerable<ResourceEntry> Resources { get; init; } = [];
}

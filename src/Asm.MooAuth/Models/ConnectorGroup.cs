namespace Asm.MooAuth.Models;

public record ConnectorGroup
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }
}

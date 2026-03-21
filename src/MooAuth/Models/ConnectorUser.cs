namespace Asm.MooAuth.Models;

public record ConnectorUser
{
    public required string Id { get; init; }

    public required string Email { get; init; }

    public required string DisplayName { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }
}

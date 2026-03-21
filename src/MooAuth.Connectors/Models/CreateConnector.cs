namespace Asm.MooAuth.Modules.Connectors.Models;

public abstract record CreateConnector<T>
{
    public required string Name { get; init; }

    public required T Config { get; init; }

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public string? Audience { get; init; }
}

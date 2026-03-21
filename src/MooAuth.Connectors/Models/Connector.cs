namespace Asm.MooAuth.Modules.Connectors.Models;
public abstract record Connector<T> : Named
{
    public required int Id { get; init; }

    public required T Config { get; init; }

    public required string Slug { get; init; }

    public required string ClientId { get; init; }

    public string? Audience { get; init; }
}

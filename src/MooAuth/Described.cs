namespace Asm.MooAuth;

public record Described : Named, IDescribed
{
    public string? Description { get; init; }
}

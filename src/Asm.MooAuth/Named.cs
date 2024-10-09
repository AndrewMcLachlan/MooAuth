namespace Asm.MooAuth;
public record Named : INamed
{
    public required string Name { get; init; }
}

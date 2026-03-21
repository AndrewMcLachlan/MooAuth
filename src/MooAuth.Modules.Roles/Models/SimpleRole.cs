namespace Asm.MooAuth.Modules.Roles.Models;
public record SimpleRole : INamed
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}

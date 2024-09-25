namespace Asm.MooAuth.Modules.Roles.Models;

public record CreateRole
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}

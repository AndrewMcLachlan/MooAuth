namespace Asm.MooAuth.Modules.Applications.Models;
public record CreatePermission
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}

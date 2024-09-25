namespace Asm.MooAuth.Modules.Applications.Models;

public record CreateApplication
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? LogoUrl { get; init; }
}

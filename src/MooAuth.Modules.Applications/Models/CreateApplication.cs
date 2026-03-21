namespace Asm.MooAuth.Modules.Applications.Models;

public record CreateApplication : Described
{
    public string? LogoUrl { get; init; }
}

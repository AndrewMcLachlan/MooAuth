namespace Asm.MooAuth.Modules.Connectors.Models.Entra;

public record EntraConfig
{
    public required Guid TenantId { get; init; }
}

namespace Asm.MooAuth.Connector.Entra;

internal record EntraConfig
{
    public required Guid TenantId { get; init; }
}

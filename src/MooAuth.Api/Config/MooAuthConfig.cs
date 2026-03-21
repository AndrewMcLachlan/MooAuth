namespace Asm.MooAuth.Api.Config;

public record MooAuthConfig
{
    public required SecretManagerConfig SecretManager { get; set;}
}

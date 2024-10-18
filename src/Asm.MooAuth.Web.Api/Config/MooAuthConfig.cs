namespace Asm.MooAuth.Web.Api.Config;

public record MooAuthConfig
{
    public required SecretManagerConfig SecretManager { get; set;}
}

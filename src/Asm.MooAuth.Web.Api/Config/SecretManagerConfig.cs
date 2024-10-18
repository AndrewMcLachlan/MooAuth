namespace Asm.MooAuth.Web.Api.Config;

public record SecretManagerConfig
{
    public required SecretManagerType Type { get; init; }

    public Uri? Uri { get; init; }

    public string? ConnectionString { get; init; }
}

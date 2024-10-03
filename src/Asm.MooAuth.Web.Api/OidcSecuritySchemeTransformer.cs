using Asm.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Asm.MooAuth.Web.Api;

internal sealed class OidcSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider, IOptions<AzureOAuthOptions> oAuthOptions) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            var requirements = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["oidc"] = new OpenApiSecurityScheme
                {
                    Name = "oidc",
                    Description = "OIDC",
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri($"{oAuthOptions.Value.Domain}{oAuthOptions.Value.TenantId}/v2.0/.well-known/openid-configuration"),
                    Scheme = "bearer", // "bearer" refers to the header name here
                    In = ParameterLocation.Header,
                    BearerFormat = "Json Web Token",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{oAuthOptions.Value.Domain}{oAuthOptions.Value.TenantId}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"{oAuthOptions.Value.Domain}{oAuthOptions.Value.TenantId}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                           {
                               { "openid", "openid" },
                               { "profile", "profile" },
                               { "email", "email" },
                               { "offline_access", "offline_access" },
                           },
                        },
                        Implicit = null,
                    },
                }
            };
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = requirements;
            document.SecurityRequirements.Add(new OpenApiSecurityRequirement()
            {
                { new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference() { Id = "oidc", Type = ReferenceType.SecurityScheme },
                }, new List<string>() },
            });
        }
    }
}

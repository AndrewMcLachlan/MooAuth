
using System.Text.Json.Serialization;
using Asm.AspNetCore.Modules;
using Asm.MooAuth;
using Asm.MooAuth.Web.Api;
using Asm.MooAuth.Web.Api.Config;
using Asm.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

MooAuthConfig mooAuthConfig;

WebApplicationStart.Run(args, "Asm.MooAuth.Web.Api", AddServices, AddApp);

void AddServices(WebApplicationBuilder builder)
{
    mooAuthConfig = builder.Configuration.GetSection("MooAuth").Get<MooAuthConfig>() ?? throw new InvalidOperationException("MooAuth config not defined");

    builder.RegisterModules(() =>
    [
        new Asm.MooAuth.Modules.Applications.Module(),
        new Asm.MooAuth.Modules.Connectors.Module(),
        new Asm.MooAuth.Modules.Roles.Module(),
        new Asm.MooAuth.Modules.Users.Module(),
    ]);

    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

    AzureOAuthOptions oAuthOptions = builder.Configuration.GetSection("MooAuth:OAuth").Get<AzureOAuthOptions>() ?? throw new InvalidOperationException("OAuth config not defined");

    builder.Services.AddPrincipalProvider();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddAzureADBearer(options => options.AzureOAuthOptions = oAuthOptions);

    builder.Services.AddAuthorization();

    builder.Services.AddMooAuthDbContext(builder.Environment, builder.Configuration);

    builder.Services.AddRepositories();
    builder.Services.AddEntities();

    builder.Services.AddProblemDetailsFactory();

    builder.Services.Configure<AzureOAuthOptions>(builder.Configuration.GetSection("MooAuth:OAuth"));

    AddSecretManager(builder);

    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer<OidcSecuritySchemeTransformer>();
    });
}

void AddSecretManager(WebApplicationBuilder builder)
{
    switch (mooAuthConfig.SecretManager.Type)
    {
        case SecretManagerType.AzureKeyVault:
            builder.AddKeyVaultSecretManager(mooAuthConfig.SecretManager.Uri ?? throw new InvalidOperationException("Unable to add Key Vault Secret Manager. Specify URI"));
            break;
        case SecretManagerType.Database:
            throw new NotSupportedException("Database secret manager not supported yet.");
        default:
            throw new InvalidOperationException("Unable to add a secret manager.");
    }
}

void AddApp(WebApplication app)
{
    app.MapOpenApi();

    IEndpointRouteBuilder builder = app.MapGroup("/api")
        .WithOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.OAuthClientId(app.Configuration["OAuth:Audience"]);
        options.OAuthAppName("MooAuth");
        options.OAuthUsePkce();
        options.OAuthScopes("api://mooauth.mclachlan.family/.default");
    });

    app.UseStandardExceptionHandler();

    app.UseAuthentication();
    app.UseSerilogEnrichWithUser();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseAuthorization();

    builder.MapModuleEndpoints();

    app.UseSecurityHeaders();

    app.MapFallbackToFile("/index.html");
}


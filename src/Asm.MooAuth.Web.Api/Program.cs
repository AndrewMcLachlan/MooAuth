
using System.Text.Json.Serialization;
using Asm.AspNetCore.Api;
using Asm.AspNetCore.Authentication;
using Asm.AspNetCore.Modules;
using Asm.MooAuth;
using Asm.MooAuth.Connector.Entra;
using Asm.MooAuth.Web.Api.Config;
using Asm.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

MooAuthConfig mooAuthConfig;

WebApplicationStart.Run(args, "Asm.MooAuth.Web.Api", AddServices, AddApp, AddHealthChecks);

void AddServices(WebApplicationBuilder builder)
{
    mooAuthConfig = builder.Configuration.GetSection("MooAuth").Get<MooAuthConfig>() ?? throw new InvalidOperationException("MooAuth config not defined");

    builder.RegisterModules(() =>
    [
        new Asm.MooAuth.Modules.Actors.Module(),
        new Asm.MooAuth.Modules.Applications.Module(),
        new Asm.MooAuth.Modules.Connectors.Module(),
        new Asm.MooAuth.Modules.DataSources.Module(),
        new Asm.MooAuth.Modules.Groups.Module(),
        new Asm.MooAuth.Modules.Roles.Module(),
        new Asm.MooAuth.Modules.Users.Module(),
    ]);

    builder.Services.AddScoped<IConnectorFactory, EntraConnectorFactory>();

    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

    AzureOAuthOptions oAuthOptions = builder.Configuration.GetSection("MooAuth:OAuth").Get<AzureOAuthOptions>() ?? throw new InvalidOperationException("OAuth config not defined");

    builder.Services.AddPrincipalProvider();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddStandardJwtBearer(options => options.OAuthOptions = oAuthOptions);

    builder.Services.AddAuthorization();

    builder.Services.AddMooAuthDbContext(builder.Environment, builder.Configuration);

    builder.Services.AddRepositories();
    builder.Services.AddEntities();

    builder.Services.AddProblemDetailsFactory();

    builder.Services.AddAzureOAuthOptions("MooAuth:OAuth");

    AddSecretManager(builder);

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
    });


    builder.Services.AddOpenApi(options =>
    {
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
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
            builder.Services.AddDatabaseSecretManager();
            break;
        default:
            throw new InvalidOperationException("Unable to add a secret manager.");
    }
}

void AddApp(WebApplication app)
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "MooAuth API");
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

    IEndpointRouteBuilder builder = app.MapGroup("/api");

    builder.MapGet("config", (IOptions<AzureOAuthOptions> options) =>
    {
        return options.Value;
    })
    .WithNames("Get Configuration")
    .WithSummary("Get API configuration")
    .WithDescription("Get configuration information for the API.")
    .WithTags("Configuration")
    .AllowAnonymous();

    builder.MapModuleEndpoints();

    app.UseSecurityHeaders();

    app.MapFallbackToFile("/index.html");
}

void AddHealthChecks(IHealthChecksBuilder builder, WebApplicationBuilder app)
{
}

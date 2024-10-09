
using Asm.AspNetCore.Modules;
using Asm.MooAuth.Web.Api;
using Asm.OAuth;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;

WebApplicationStart.Run(args, "MooAuth", AddServices, AddApp);

void AddServices(WebApplicationBuilder builder)
{
    builder.RegisterModules(() => new IModule[]
    {
        new Asm.MooAuth.Modules.Applications.Module(),
        new Asm.MooAuth.Modules.Roles.Module(),
        new Asm.MooAuth.Modules.Users.Module(),
    });

    AzureOAuthOptions oAuthOptions = builder.Configuration.GetSection("OAuth").Get<AzureOAuthOptions>() ?? throw new InvalidOperationException("OAuth config not defined");

    builder.Services.AddPrincipalProvider();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddAzureADBearer(options => options.AzureOAuthOptions = oAuthOptions);

    builder.Services.AddAuthorization();

    builder.Services.AddMooAuthDbContext(builder.Environment, builder.Configuration);

    builder.Services.AddRepositories();
    builder.Services.AddEntities();

    builder.Services.AddProblemDetailsFactory();

    builder.Services.Configure<AzureOAuthOptions>(builder.Configuration.GetSection("OAuth"));

    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer<OidcSecuritySchemeTransformer>();
    });
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


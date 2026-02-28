using Asm.MooAuth.AppHost.Configuration;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var sqlConfig = builder.Configuration.GetSection("SqlServer").Get<SqlServer>() ?? new SqlServer();

IResourceBuilder<IResourceWithConnectionString> mooAuthDb;

if (sqlConfig.Enabled)
{
    // Set up SQL Server container

    var sql = builder.AddSqlServer("sql-server", port: 62980)
        .WithLifetime(ContainerLifetime.Persistent);

    if (String.IsNullOrWhiteSpace(sqlConfig.DataBindMount))
    {
        sql = sql.WithDataVolume("MooAuthData");
    }
    else
    {
        sql = sql.WithDataBindMount(sqlConfig.DataBindMount);
    }

    mooAuthDb = sql.AddDatabase("MooAuth");

    //builder.AddSqlProject<Projects.Asm_MooAuth_Database>("mooauth-database-project")
    //.WithReference(mooAuthDb);
}
else
{
    // Use an existing SQL Server

    mooAuthDb = builder.AddConnectionString("MooAuth");

    //builder.AddSqlProject<Projects.Asm_MooAuth_Database>("mooauth-database-project")
    //.WithReference(mooAuthDb);
}

var api = builder.AddProject<Projects.Asm_MooAuth_Web_Api>("mooauth-api", "API Only")
    .WithReference(mooAuthDb)
    .WithHttpHealthCheck("/healthz");

if (sqlConfig.Enabled)
{
    api.WaitFor(mooAuthDb);
}

builder.AddJavaScriptApp("mooauth-app", "../Asm.MooAuth.Web.App", "start")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpsEndpoint(port: 3006, isProxied: false);

builder.Build().Run();


builder.Build().Run();

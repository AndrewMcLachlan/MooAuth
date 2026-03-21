using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Asm.MooAuth.Secrets.Database;

/// <summary>
/// Manages secrets stored in a database with encryption via ASP.NET Core Data Protection.
/// </summary>
/// <remarks>
/// Data Protection keys are stored in the user profile directory on Windows (%LOCALAPPDATA%\ASP.NET\DataProtection-Keys).
/// This provides reasonable security for development/testing while being simple to set up.
/// For production, consider using Azure Key Vault or configuring Data Protection key storage appropriately.
/// </remarks>
public class DatabaseSecretManager : ISecretManager
{
    private const string ProtectorPurpose = "MooAuth.Secrets.v1";
    private readonly IDataProtector _protector;
    private readonly string _connectionString;

    public DatabaseSecretManager(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
    {
        _protector = dataProtectionProvider.CreateProtector(ProtectorPurpose);
        _connectionString = configuration.GetConnectionString("MooAuth")
            ?? throw new InvalidOperationException("MooAuth connection string not found");
    }

    public async Task<string> GetSecretAsync(string key, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT [Value] FROM [Secret] WHERE [Key] = @Key";
        command.Parameters.Add(new SqlParameter("@Key", key));

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result is null or DBNull)
        {
            throw new NotFoundException($"Secret '{key}' not found");
        }

        var encrypted = (string)result;
        return _protector.Unprotect(encrypted);
    }

    public async Task SetSecretAsync(string key, string secret, CancellationToken cancellationToken = default)
    {
        var encrypted = _protector.Protect(secret);

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = """
            MERGE [Secret] AS target
            USING (SELECT @Key AS [Key]) AS source
            ON target.[Key] = source.[Key]
            WHEN MATCHED THEN
                UPDATE SET [Value] = @Value, [Modified] = SYSUTCDATETIME()
            WHEN NOT MATCHED THEN
                INSERT ([Key], [Value], [Created], [Modified])
                VALUES (@Key, @Value, SYSUTCDATETIME(), SYSUTCDATETIME());
            """;
        command.Parameters.Add(new SqlParameter("@Key", key));
        command.Parameters.Add(new SqlParameter("@Value", encrypted));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteSecretAsync(string key, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM [Secret] WHERE [Key] = @Key";
        command.Parameters.Add(new SqlParameter("@Key", key));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}

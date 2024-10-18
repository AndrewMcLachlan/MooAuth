using Asm.MooAuth.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Asm.MooAuth.Secrets.Database;

/// <summary>
/// This class is responsible for managing secrets stored in a database.
/// </summary>
/// <remarks>
/// This class is only half the story and included only as an example. Key management for the <see cref="IDataProtectionProvider"> is not implemented.
/// </remarks>
/// <param name="dataProtectionProvider"></param>
/// <param name="context"></param>
public class DataBaseSecretManager(IDataProtectionProvider dataProtectionProvider, MooAuthContext context) : ISecretManager
{
    public async Task<string> GetSecretAsync(string key, CancellationToken cancellationToken = default)
    {
        var protector = GetDataProtector();

        var connection = context.Database.GetDbConnection();

        var command = connection.CreateCommand();

        command.CommandText = $"SELECT Secret FROM Secrets WHERE Key = @key";
        command.Parameters.Add(new SqlParameter("@key", key));

        var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (reader.NextResult())
        {
            var encrypted = reader.GetString(0);
            return protector.Unprotect(encrypted);
        }
        else
        {
            throw new NotFoundException();
        }
    }

    public Task SetSecretAsync(string key, string secret, CancellationToken cancellationToken = default)
    {
        var protector = GetDataProtector();
        var encrypted = protector.Protect(secret);

        return context.Database.ExecuteSqlAsync($"INSERT INTO Secrets (Key, Secret) VALUES ({key}, {encrypted})", cancellationToken);
    }

    public Task DeleteSecretAsync(string key, CancellationToken cancellationToken = default) =>
        context.Database.ExecuteSqlAsync($"DELETE FROM Secrets WHERE Key = {key}", cancellationToken);

    private IDataProtector GetDataProtector() =>
        dataProtectionProvider.CreateProtector("SecretsProtector");
}

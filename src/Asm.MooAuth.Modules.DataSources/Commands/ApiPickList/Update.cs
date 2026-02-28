using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.ApiPickList;

public record Update(int Id, [FromBody] CreateApiPickListDataSource DataSource) : ICommand<ApiPickListDataSource>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository, ISecretManager secretManager) : ICommandHandler<Update, ApiPickListDataSource>
{
    public async ValueTask<ApiPickListDataSource> Handle(Update command, CancellationToken cancellationToken)
    {
        var entity = await repository.Get(command.Id, cancellationToken)
            ?? throw new NotFoundException();

        entity.Name = command.DataSource.Name;
        entity.Description = command.DataSource.Description;
        entity.SetConfig(command.DataSource.Config);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Update secret if provided
        var secret = command.DataSource.Config.AuthType switch
        {
            ApiAuthType.ApiKey => command.DataSource.ApiKey,
            ApiAuthType.OAuthClientCredentials => command.DataSource.ClientSecret,
            _ => null
        };

        if (!String.IsNullOrEmpty(secret))
        {
            await secretManager.SetSecretAsync($"datasource-{entity.Key}-secret", secret, cancellationToken);
        }

        return entity.ToApiPickListModel();
    }
}

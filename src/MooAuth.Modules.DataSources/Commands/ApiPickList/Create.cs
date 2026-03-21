using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.ApiPickList;

public record Create([FromBody] CreateApiPickListDataSource DataSource) : ICommand<ApiPickListDataSource>;

internal class CreateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository, ISecretManager secretManager) : ICommandHandler<Create, ApiPickListDataSource>
{
    public async ValueTask<ApiPickListDataSource> Handle(Create command, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.DataSources.DataSource
        {
            Name = command.DataSource.Name,
            Description = command.DataSource.Description,
            Key = command.DataSource.Key.ToMachine(),
            DataSourceType = MooAuth.Models.DataSourceType.ApiPickList,
        };

        entity.SetConfig(command.DataSource.Config);

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Store secret based on auth type
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

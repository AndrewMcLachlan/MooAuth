using Asm.Domain;
using Asm.MooAuth.Modules.Connectors.Models.Entra;
using Microsoft.AspNetCore.Mvc;
using IConnectorRepository = Asm.MooAuth.Domain.Entities.Connectors.IConnectorRepository;

namespace Asm.MooAuth.Modules.Connectors.Commands.Entra;
public record Create([FromBody] CreateEntraConnector Connector) : ICommand<EntraConnector>;

internal class CreateHandler(IUnitOfWork unitOfWork, IConnectorRepository repository, ISecretManager secretManager) : ICommandHandler<Create, EntraConnector>
{
    public async ValueTask<EntraConnector> Handle(Create command, CancellationToken cancellationToken)
    {
        var connector = new Domain.Entities.Connectors.Connector
        {
            Name = command.Connector.Name,
            ClientId = command.Connector.ClientId,
            Slug = command.Connector.Name.ToMachine(),
        };

        connector.SetConfig(command.Connector.Config);

        repository.Add(connector);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await secretManager.SetSecretAsync($"{connector.Slug}-{command.Connector.ClientId}", command.Connector.ClientSecret, cancellationToken);

        return connector.ToEntraModel();
    }
}

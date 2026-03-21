using Asm.Domain;
using Asm.MooAuth.Modules.Connectors.Models.Entra;
using Microsoft.AspNetCore.Mvc;
using IConnectorRepository = Asm.MooAuth.Domain.Entities.Connectors.IConnectorRepository;

namespace Asm.MooAuth.Modules.Connectors.Commands.Entra;

public record Update([FromRoute] int Id, [FromBody] CreateEntraConnector Connector) : ICommand<EntraConnector>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IConnectorRepository repository, ISecretManager secretManager) : ICommandHandler<Update, EntraConnector>
{
    public async ValueTask<EntraConnector> Handle(Update command, CancellationToken cancellationToken)
    {
        var connector = await repository.Get(command.Id, cancellationToken) ?? throw new NotFoundException();

        connector.Name = command.Connector.Name;
        connector.ClientId = command.Connector.ClientId;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await secretManager.SetSecretAsync($"{connector.Slug}-{command.Connector.ClientId}", command.Connector.ClientSecret, cancellationToken);

        return connector.ToEntraModel();
    }
}

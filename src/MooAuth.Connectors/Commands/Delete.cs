using Asm.Domain;
using IConnectorRepository = Asm.MooAuth.Domain.Entities.Connectors.IConnectorRepository;

namespace Asm.MooAuth.Modules.Connectors.Commands;

public record Delete(int Id) : ICommand;

internal class DeleteHandler(IUnitOfWork unitOfWork, IConnectorRepository repository) : ICommandHandler<Delete>
{
    public async ValueTask Handle(Delete command, CancellationToken cancellationToken)
    {
        repository.Delete(command.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

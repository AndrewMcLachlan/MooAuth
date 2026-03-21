using Asm.Domain;
using IPermissionRepository = Asm.MooAuth.Domain.Entities.Permissions.IPermissionRepository;

namespace Asm.MooAuth.Modules.Applications.Commands.Permissions;

public record Delete(int ApplicationId, int Id) : ICommand;

internal class DeleteHandler(IUnitOfWork unitOfWork, IPermissionRepository repository) : ICommandHandler<Delete>
{
    public async ValueTask Handle(Delete command, CancellationToken cancellationToken)
    {
        repository.Delete(command.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

using Asm.Domain;
using IRoleRepository = Asm.MooAuth.Domain.Entities.Roles.IRoleRepository;

namespace Asm.MooAuth.Modules.Roles.Commands.Roles;

public record Delete(int Id) : ICommand;

internal class DeleteHandler(IUnitOfWork unitOfWork, IRoleRepository repository) : ICommandHandler<Delete>
{
    public async ValueTask Handle(Delete command, CancellationToken cancellationToken)
    {
        repository.Delete(command.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

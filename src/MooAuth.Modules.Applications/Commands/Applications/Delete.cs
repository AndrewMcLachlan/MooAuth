using Asm.Domain;
using IApplicationRepository = Asm.MooAuth.Domain.Entities.Applications.IApplicationRepository;

namespace Asm.MooAuth.Modules.Applications.Commands.Applications;

public record Delete(int Id) : ICommand;

internal class DeleteHandler(IUnitOfWork unitOfWork, IApplicationRepository repository) : ICommandHandler<Delete>
{
    public async ValueTask Handle(Delete command, CancellationToken cancellationToken)
    {
        repository.Delete(command.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

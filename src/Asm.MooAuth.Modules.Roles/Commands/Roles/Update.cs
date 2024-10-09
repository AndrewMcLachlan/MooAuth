using Asm.MooAuth.Modules.Roles.Models;
using IRoleRepository = Asm.MooAuth.Domain.Entities.Roles.IRoleRepository;

namespace Asm.MooAuth.Modules.Roles.Commands.Roles;

public record Update(int Id, CreateRole Role) : ICommand<Role>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IRoleRepository repository) : ICommandHandler<Update, Role>
{
    public async ValueTask<Role> Handle(Update command, CancellationToken cancellationToken)
    {
        var role = await repository.Get(command.Id, cancellationToken) ?? throw new NotFoundException();

        role.Name = command.Role.Name;
        role.Description = command.Role.Description;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return role.ToModel();
    }
}

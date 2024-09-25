using Asm.Domain;
using Asm.MooAuth.Modules.Roles.Models;
using IRoleRepository = Asm.MooAuth.Domain.Entities.Roles.IRoleRepository;

namespace Asm.MooAuth.Modules.Roles.Commands.Roles;
public record Create(CreateRole Role) : ICommand<Role>;

internal class CreateHandler(IUnitOfWork unitOfWork, IRoleRepository repository) : ICommandHandler<Create, Role>
{
    public async ValueTask<Role> Handle(Create command, CancellationToken cancellationToken)
    {
        var role = new Domain.Entities.Roles.Role
        {
            Name = command.Role.Name,
            Description = command.Role.Description,
        };

        repository.Add(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return role.ToModel();
    }
}

using Asm.Domain;
using Asm.MooAuth.Domain.Entities.Permissions;
using Asm.MooAuth.Domain.Entities.Roles;

namespace Asm.MooAuth.Modules.Roles.Commands.Roles;

public record AddPermission(int RoleId, int Id) : ICommand;

internal class AddPermissionHandler(IUnitOfWork unitOfWork, IRoleRepository repository, IPermissionRepository permissionRepository) : ICommandHandler<AddPermission>
{
    public async ValueTask Handle(AddPermission command, CancellationToken cancellationToken)
    {
        var role = await repository.Get(command.RoleId, cancellationToken) ?? throw new NotFoundException();

        if (role.Permissions.Any(p => p.Id == command.Id)) throw new ExistsException("Permission already exists");

        var permission = await permissionRepository.Get(command.Id, cancellationToken) ?? throw new NotFoundException();

        role.Permissions.Add(permission);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

using Asm.MooAuth.Domain.Entities.Roles;

namespace Asm.MooAuth.Modules.Roles.Commands.Roles;
public record RemovePermission(int RoleId, int PermissionId) : ICommand;

internal class RemovePermissionHandler(IUnitOfWork unitOfWork, IRoleRepository repository) : ICommandHandler<RemovePermission>
{
    public async ValueTask Handle(RemovePermission command, CancellationToken cancellationToken)
    {
        var role = await repository.Get(command.RoleId, new IncludePermissionsSpecification(), cancellationToken) ?? throw new NotFoundException();

        var permission = role.Permissions.SingleOrDefault(p => p.Id == command.PermissionId) ?? throw new NotFoundException("Permission not found");

        role.Permissions.Remove(permission);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

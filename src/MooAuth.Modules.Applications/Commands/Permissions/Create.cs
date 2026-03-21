using Asm.Domain;
using IPermissionRepository = Asm.MooAuth.Domain.Entities.Permissions.IPermissionRepository;
using Asm.MooAuth.Modules.Applications.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asm.MooAuth.Modules.Applications.Commands.Permissions;
public record Create([FromRoute] int applicationId, [FromBody] CreatePermission Permission) : ICommand<Permission>;

internal class CreateHandler(IUnitOfWork unitOfWork, IPermissionRepository repository) : ICommandHandler<Create, Permission>
{
    public async ValueTask<Permission> Handle(Create command, CancellationToken cancellationToken)
    {
        var permission = new Domain.Entities.Permissions.Permission
        {
            Name = command.Permission.Name,
            Description = command.Permission.Description,
            ApplicationId = command.applicationId,
        };

        repository.Add(permission);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return permission.ToModel();
    }
}

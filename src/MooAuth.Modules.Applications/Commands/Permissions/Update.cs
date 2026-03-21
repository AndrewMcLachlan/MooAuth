using Asm.Domain;
using Asm.MooAuth.Modules.Applications.Models;
using Microsoft.AspNetCore.Mvc;
using IPermissionRepository = Asm.MooAuth.Domain.Entities.Permissions.IPermissionRepository;

namespace Asm.MooAuth.Modules.Applications.Commands.Permissions;

public record Update([FromRoute]int ApplicationId, [FromRoute]int Id, [FromBody]CreatePermission Permission) : ICommand<Permission>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IPermissionRepository repository) : ICommandHandler<Update, Permission>
{
    public async ValueTask<Permission> Handle(Update command, CancellationToken cancellationToken)
    {
        var permission = await repository.Get(command.Id, cancellationToken) ?? throw new NotFoundException();

        permission.Name = command.Permission.Name;
        permission.Description = command.Permission.Description;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.ToModel();
    }
}

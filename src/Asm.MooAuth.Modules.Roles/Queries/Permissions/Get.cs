using Asm.MooAuth.Modules.Roles.Models;

namespace Asm.MooAuth.Modules.Roles.Queries.Permissions;

public record Get(int RoleId, int Id) : IQuery<Permission>;

internal class GetHandler(IQueryable<Domain.Entities.Roles.Role> roles) : IQueryHandler<Get, Permission>
{
    public async ValueTask<Permission> Handle(Get query, CancellationToken cancellationToken) =>
        (await roles
            .Where(a => a.Id == query.RoleId)
            .SelectMany(a => a.Permissions)
            .Where(p => p.Id == query.Id)
            .ToModel()
            .FirstOrDefaultAsync(cancellationToken)) ?? throw new NotFoundException();
}

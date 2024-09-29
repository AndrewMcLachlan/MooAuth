using Asm.MooAuth.Modules.Roles.Models;

namespace Asm.MooAuth.Modules.Roles.Queries;

public record Get(int Id) : IQuery<Role>;

internal class GetHandler(IQueryable<Domain.Entities.Roles.Role> roles) : IQueryHandler<Get, Role>
{
    public async ValueTask<Role> Handle(Get query, CancellationToken cancellationToken) =>
        (await roles
            .Specify(new Domain.Entities.Roles.IncludePermissionsSpecification())
            .Where(a => a.Id == query.Id)
            .ToModel()
            .FirstOrDefaultAsync(cancellationToken)) ?? throw new NotFoundException();
}

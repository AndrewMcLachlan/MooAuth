using Asm.MooAuth.Modules.Roles.Models;

namespace Asm.MooAuth.Modules.Roles.Queries;

public record GetAll() : IQuery<IEnumerable<Role>>;

internal class GetAllHandler(IQueryable<Domain.Entities.Roles.Role> roles) : IQueryHandler<GetAll, IEnumerable<Role>>
{
    public async ValueTask<IEnumerable<Role>> Handle(GetAll query, CancellationToken cancellationToken) =>
        await roles.Specify(new Domain.Entities.Roles.IncludePermissionsSpecification()).ToModel().ToListAsync(cancellationToken);
}

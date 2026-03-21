using Asm.MooAuth.Modules.Applications.Models;

namespace Asm.MooAuth.Modules.Applications.Queries.Permissions;

public record Get(int ApplicationId, int Id) : IQuery<Permission>;

internal class GetHandler(IQueryable<Domain.Entities.Applications.Application> applications) : IQueryHandler<Get, Permission>
{
    public async ValueTask<Permission> Handle(Get query, CancellationToken cancellationToken) =>
        (await applications
            .Where(a => a.Id == query.ApplicationId)
            .SelectMany(a => a.Permissions)
            .Where(p => p.Id == query.Id)
            .ToModel()
            .FirstOrDefaultAsync(cancellationToken)) ?? throw new NotFoundException();
}

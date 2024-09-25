using Asm.MooAuth.Modules.Applications.Models;

namespace Asm.MooAuth.Modules.Applications.Queries;

public record Get(int Id) : IQuery<Application>;

internal class GetHandler(IQueryable<Domain.Entities.Applications.Application> applications) : IQueryHandler<Get, Application>
{
    public async ValueTask<Application> Handle(Get query, CancellationToken cancellationToken) =>
        (await applications
            .Where(a => a.Id == query.Id)
            .ToModel()
            .FirstOrDefaultAsync(cancellationToken)) ?? throw new NotFoundException();
}

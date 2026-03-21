using Asm.MooAuth.Modules.Applications.Models;

namespace Asm.MooAuth.Modules.Applications.Queries;

public record GetAll() : IQuery<IEnumerable<Application>>;

internal class GetAllHandler(IQueryable<Domain.Entities.Applications.Application> applications) : IQueryHandler<GetAll, IEnumerable<Application>>
{
    public async ValueTask<IEnumerable<Application>> Handle(GetAll query, CancellationToken cancellationToken) =>
        await applications.ToModel().ToListAsync(cancellationToken);
}

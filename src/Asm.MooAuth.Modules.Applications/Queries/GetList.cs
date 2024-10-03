using Asm.MooAuth.Modules.Applications.Models;

namespace Asm.MooAuth.Modules.Applications.Queries;

public record GetList() : IQuery<IEnumerable<SimpleApplication>>;

internal class GetListHandler(IQueryable<Domain.Entities.Applications.Application> applications) : IQueryHandler<GetList, IEnumerable<SimpleApplication>>
{
    public async ValueTask<IEnumerable<SimpleApplication>> Handle(GetList query, CancellationToken cancellationToken)
    {
        return await applications.Include(a => a.Permissions).ToSimpleModel().ToListAsync(cancellationToken);
    }
}

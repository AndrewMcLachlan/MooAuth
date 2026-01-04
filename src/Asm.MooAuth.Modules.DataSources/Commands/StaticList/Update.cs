using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.StaticList;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.StaticList;

public record Update(int Id, [FromBody] CreateStaticListDataSource DataSource) : ICommand<StaticListDataSource>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Update, StaticListDataSource>
{
    public async ValueTask<StaticListDataSource> Handle(Update command, CancellationToken cancellationToken)
    {
        var entity = await repository.Get(command.Id, cancellationToken)
            ?? throw new NotFoundException();

        entity.Name = command.DataSource.Name;
        entity.Description = command.DataSource.Description;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToStaticListModel();
    }
}

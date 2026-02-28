using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.PickList;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.PickList;

public record Update(int Id, [FromBody] CreatePickListDataSource DataSource) : ICommand<PickListDataSource>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Update, PickListDataSource>
{
    public async ValueTask<PickListDataSource> Handle(Update command, CancellationToken cancellationToken)
    {
        var entity = await repository.Get(command.Id, cancellationToken)
            ?? throw new NotFoundException();

        entity.Name = command.DataSource.Name;
        entity.Description = command.DataSource.Description;
        entity.SetConfig(new PickListConfig { AllowMultiple = command.DataSource.AllowMultiple });

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToPickListModel();
    }
}

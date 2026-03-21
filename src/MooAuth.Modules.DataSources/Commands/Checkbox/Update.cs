using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.Checkbox;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.Checkbox;

public record Update(int Id, [FromBody] CreateCheckboxDataSource DataSource) : ICommand<CheckboxDataSource>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Update, CheckboxDataSource>
{
    public async ValueTask<CheckboxDataSource> Handle(Update command, CancellationToken cancellationToken)
    {
        var entity = await repository.Get(command.Id, cancellationToken)
            ?? throw new NotFoundException();

        entity.Name = command.DataSource.Name;
        entity.Description = command.DataSource.Description;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToCheckboxModel();
    }
}

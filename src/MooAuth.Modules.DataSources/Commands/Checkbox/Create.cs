using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.Checkbox;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.Checkbox;

public record Create([FromBody] CreateCheckboxDataSource DataSource) : ICommand<CheckboxDataSource>;

internal class CreateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Create, CheckboxDataSource>
{
    public async ValueTask<CheckboxDataSource> Handle(Create command, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.DataSources.DataSource
        {
            Name = command.DataSource.Name,
            Description = command.DataSource.Description,
            Key = command.DataSource.Key.ToMachine(),
            DataSourceType = MooAuth.Models.DataSourceType.Checkbox,
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToCheckboxModel();
    }
}

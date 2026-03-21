using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.FreeText;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.FreeText;

public record Create([FromBody] CreateFreeTextDataSource DataSource) : ICommand<FreeTextDataSource>;

internal class CreateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Create, FreeTextDataSource>
{
    public async ValueTask<FreeTextDataSource> Handle(Create command, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.DataSources.DataSource
        {
            Name = command.DataSource.Name,
            Description = command.DataSource.Description,
            Key = command.DataSource.Key.ToMachine(),
            DataSourceType = MooAuth.Models.DataSourceType.FreeText,
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToFreeTextModel();
    }
}

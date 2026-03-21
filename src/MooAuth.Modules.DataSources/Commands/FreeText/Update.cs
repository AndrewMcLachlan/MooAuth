using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.FreeText;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.FreeText;

public record Update(int Id, [FromBody] CreateFreeTextDataSource DataSource) : ICommand<FreeTextDataSource>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Update, FreeTextDataSource>
{
    public async ValueTask<FreeTextDataSource> Handle(Update command, CancellationToken cancellationToken)
    {
        var entity = await repository.Get(command.Id, cancellationToken)
            ?? throw new NotFoundException();

        entity.Name = command.DataSource.Name;
        entity.Description = command.DataSource.Description;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToFreeTextModel();
    }
}

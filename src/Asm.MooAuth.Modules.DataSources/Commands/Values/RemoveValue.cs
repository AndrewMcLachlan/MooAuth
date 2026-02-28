using Asm.Domain;
using Asm.MooAuth.Domain.Entities.DataSources;
using Microsoft.AspNetCore.Http;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.Values;

public record RemoveValue(int DataSourceId, int ValueId) : ICommand;

internal class RemoveValueHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<RemoveValue>
{
    public async ValueTask Handle(RemoveValue command, CancellationToken cancellationToken)
    {
        var dataSource = await repository.Get(command.DataSourceId, new IncludeValuesSpecification(), cancellationToken)
            ?? throw new NotFoundException();

        if (dataSource.DataSourceType != MooAuth.Models.DataSourceType.PickList)
        {
            throw new BadHttpRequestException("Values can only be removed from pick list data sources");
        }

        var value = dataSource.Values.FirstOrDefault(v => v.Id == command.ValueId)
            ?? throw new NotFoundException();

        dataSource.Values.Remove(value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

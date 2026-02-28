using Asm.Domain;
using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Modules.DataSources.Models;
using DataSourceValueModel = Asm.MooAuth.Modules.DataSources.Models.DataSourceValue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.Values;

public record AddValue(int DataSourceId, [FromBody] Models.CreateDataSourceValue Value) : ICommand<DataSourceValueModel>;

internal class AddValueHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<AddValue, DataSourceValueModel>
{
    public async ValueTask<DataSourceValueModel> Handle(AddValue command, CancellationToken cancellationToken)
    {
        var dataSource = await repository.Get(command.DataSourceId, new IncludeValuesSpecification(), cancellationToken)
            ?? throw new NotFoundException();

        if (dataSource.DataSourceType != MooAuth.Models.DataSourceType.PickList)
        {
            throw new BadHttpRequestException("Values can only be added to pick list data sources");
        }

        var value = new Domain.Entities.DataSources.DataSourceValue
        {
            Key = command.Value.Key,
            DisplayValue = command.Value.DisplayValue,
            SortOrder = command.Value.SortOrder,
        };

        dataSource.Values.Add(value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return value.ToModel();
    }
}

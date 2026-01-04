using Asm.Domain;
using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Modules.DataSources.Models;
using DataSourceValueModel = Asm.MooAuth.Modules.DataSources.Models.DataSourceValue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.Values;

public record UpdateValue(int DataSourceId, int ValueId, [FromBody] Models.CreateDataSourceValue Value) : ICommand<DataSourceValueModel>;

internal class UpdateValueHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<UpdateValue, DataSourceValueModel>
{
    public async ValueTask<DataSourceValueModel> Handle(UpdateValue command, CancellationToken cancellationToken)
    {
        var dataSource = await repository.Get(command.DataSourceId, new IncludeValuesSpecification(), cancellationToken)
            ?? throw new NotFoundException();

        if (dataSource.DataSourceType != MooAuth.Models.DataSourceType.StaticList)
        {
            throw new BadHttpRequestException("Values can only be updated on static list data sources");
        }

        var value = dataSource.Values.FirstOrDefault(v => v.Id == command.ValueId)
            ?? throw new NotFoundException();

        value.Key = command.Value.Key;
        value.DisplayValue = command.Value.DisplayValue;
        value.SortOrder = command.Value.SortOrder;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return value.ToModel();
    }
}

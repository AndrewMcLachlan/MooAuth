using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.PickList;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.PickList;

public record Create([FromBody] CreatePickListDataSource DataSource) : ICommand<PickListDataSource>;

internal class CreateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Create, PickListDataSource>
{
    public async ValueTask<PickListDataSource> Handle(Create command, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.DataSources.DataSource
        {
            Name = command.DataSource.Name,
            Description = command.DataSource.Description,
            Key = command.DataSource.Key.ToMachine(),
            DataSourceType = MooAuth.Models.DataSourceType.PickList,
            Values = command.DataSource.Values.Select(v => new Domain.Entities.DataSources.DataSourceValue
            {
                Key = v.Key,
                DisplayValue = v.DisplayValue,
                SortOrder = v.SortOrder,
            }).ToList(),
        };

        entity.SetConfig(new PickListConfig { AllowMultiple = command.DataSource.AllowMultiple });

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToPickListModel();
    }
}

using Asm.Domain;
using Asm.MooAuth.Modules.DataSources.Models.StaticList;
using Microsoft.AspNetCore.Mvc;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands.StaticList;

public record Create([FromBody] CreateStaticListDataSource DataSource) : ICommand<StaticListDataSource>;

internal class CreateHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository) : ICommandHandler<Create, StaticListDataSource>
{
    public async ValueTask<StaticListDataSource> Handle(Create command, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.DataSources.DataSource
        {
            Name = command.DataSource.Name,
            Description = command.DataSource.Description,
            Key = command.DataSource.Key.ToMachine(),
            DataSourceType = MooAuth.Models.DataSourceType.StaticList,
            Values = command.DataSource.Values.Select(v => new Domain.Entities.DataSources.DataSourceValue
            {
                Key = v.Key,
                DisplayValue = v.DisplayValue,
                SortOrder = v.SortOrder,
            }).ToList(),
        };

        repository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToStaticListModel();
    }
}

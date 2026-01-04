using Asm.Domain;
using IDataSourceRepository = Asm.MooAuth.Domain.Entities.DataSources.IDataSourceRepository;

namespace Asm.MooAuth.Modules.DataSources.Commands;

public record Delete(int Id) : ICommand;

internal class DeleteHandler(IUnitOfWork unitOfWork, IDataSourceRepository repository, ISecretManager secretManager) : ICommandHandler<Delete>
{
    public async ValueTask Handle(Delete command, CancellationToken cancellationToken)
    {
        var dataSource = await repository.Get(command.Id, cancellationToken)
            ?? throw new NotFoundException();

        // Delete any associated secrets
        if (dataSource.DataSourceType == MooAuth.Models.DataSourceType.ApiList)
        {
            try
            {
                await secretManager.DeleteSecretAsync($"datasource-{dataSource.Key}-secret", cancellationToken);
            }
            catch
            {
                // Ignore if secret doesn't exist
            }
        }

        repository.Delete(command.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

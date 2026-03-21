using Asm.Domain;
using Asm.MooAuth.Modules.Applications.Models;
using Microsoft.AspNetCore.Mvc;
using IApplicationRepository = Asm.MooAuth.Domain.Entities.Applications.IApplicationRepository;

namespace Asm.MooAuth.Modules.Applications.Commands.Applications;

public record Update([FromRoute]int Id, [FromBody]CreateApplication Application) : ICommand<Application>;

internal class UpdateHandler(IUnitOfWork unitOfWork, IApplicationRepository repository) : ICommandHandler<Update, Application>
{
    public async ValueTask<Application> Handle(Update command, CancellationToken cancellationToken)
    {
        var application = await repository.Get(command.Id, cancellationToken) ?? throw new NotFoundException();

        application.Name = command.Application.Name;
        application.Description = command.Application.Description;
        application.LogoUrl = command.Application.LogoUrl;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return application.ToModel();
    }
}

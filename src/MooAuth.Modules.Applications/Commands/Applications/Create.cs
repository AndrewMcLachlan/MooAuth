using Asm.Domain;
using Asm.MooAuth.Modules.Applications.Models;
using Microsoft.AspNetCore.Mvc;
using IApplicationRepository = Asm.MooAuth.Domain.Entities.Applications.IApplicationRepository;

namespace Asm.MooAuth.Modules.Applications.Commands.Applications;
public record Create([FromBody]CreateApplication Application) : ICommand<Application>;

internal class CreateHandler(IUnitOfWork unitOfWork, IApplicationRepository repository) : ICommandHandler<Create, Application>
{
    public async ValueTask<Application> Handle(Create command, CancellationToken cancellationToken)
    {
        var application = new Domain.Entities.Applications.Application
        {
            Name = command.Application.Name,
            Description = command.Application.Description,
            LogoUrl = command.Application.LogoUrl
        };

        repository.Add(application);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return application.ToModel();
    }
}

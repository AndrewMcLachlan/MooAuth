using Asm.MooAuth.Modules.Connectors.Models.Entra;
using FluentValidation;

namespace Asm.MooAuth.Modules.Connectors.Validators.Entra;

internal class Create : AbstractValidator<Commands.Entra.Create>
{
    public Create()
    {
        RuleFor(x => x.Connector).SetValidator(new ConnectorValidator<CreateEntraConnector, EntraConfig>());
    }
}

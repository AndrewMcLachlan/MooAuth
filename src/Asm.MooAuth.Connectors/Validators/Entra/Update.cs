using Asm.MooAuth.Modules.Connectors.Models.Entra;
using FluentValidation;

namespace Asm.MooAuth.Modules.Connectors.Validators.Entra;
internal class Update : AbstractValidator<Commands.Entra.Update>
{
    public Update()
    {
        RuleFor(x => x.Connector).SetValidator(new ConnectorValidator<CreateEntraConnector, EntraConfig>());
    }
}

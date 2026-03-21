using Asm.MooAuth.Modules.Connectors.Models;
using FluentValidation;

namespace Asm.MooAuth.Modules.Connectors.Validators;
internal class ConnectorValidator<TConnector, TConfig> : AbstractValidator<TConnector> where TConnector : CreateConnector<TConfig>
{
    public ConnectorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Config).NotNull();
    }
}

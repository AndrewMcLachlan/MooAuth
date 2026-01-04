using Asm.MooAuth.Modules.DataSources.Models.FreeText;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.FreeText;

internal class Update : AbstractValidator<Commands.FreeText.Update>
{
    public Update()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateFreeTextDataSource>());
    }
}

using Asm.MooAuth.Modules.DataSources.Models.FreeText;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.FreeText;

internal class Create : AbstractValidator<Commands.FreeText.Create>
{
    public Create()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateFreeTextDataSource>());
    }
}

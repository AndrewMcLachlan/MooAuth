using Asm.MooAuth.Modules.DataSources.Models.Checkbox;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.Checkbox;

internal class Update : AbstractValidator<Commands.Checkbox.Update>
{
    public Update()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateCheckboxDataSource>());
    }
}

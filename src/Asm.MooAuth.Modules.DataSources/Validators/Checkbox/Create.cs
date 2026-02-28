using Asm.MooAuth.Modules.DataSources.Models.Checkbox;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.Checkbox;

internal class Create : AbstractValidator<Commands.Checkbox.Create>
{
    public Create()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateCheckboxDataSource>());
    }
}

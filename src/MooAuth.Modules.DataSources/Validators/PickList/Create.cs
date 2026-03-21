using Asm.MooAuth.Modules.DataSources.Models.PickList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.PickList;

internal class Create : AbstractValidator<Commands.PickList.Create>
{
    public Create()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreatePickListDataSource>());
    }
}

using Asm.MooAuth.Modules.DataSources.Models.PickList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.PickList;

internal class Update : AbstractValidator<Commands.PickList.Update>
{
    public Update()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreatePickListDataSource>());
    }
}

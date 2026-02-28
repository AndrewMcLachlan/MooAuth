using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.ApiPickList;

internal class Update : AbstractValidator<Commands.ApiPickList.Update>
{
    public Update()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateApiPickListDataSource>());
    }
}

using Asm.MooAuth.Modules.DataSources.Models.ApiPickList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.ApiPickList;

internal class Create : AbstractValidator<Commands.ApiPickList.Create>
{
    public Create()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateApiPickListDataSource>());
    }
}

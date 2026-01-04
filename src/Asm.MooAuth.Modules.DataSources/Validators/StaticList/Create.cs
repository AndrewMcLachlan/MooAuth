using Asm.MooAuth.Modules.DataSources.Models.StaticList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.StaticList;

internal class Create : AbstractValidator<Commands.StaticList.Create>
{
    public Create()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateStaticListDataSource>());
    }
}

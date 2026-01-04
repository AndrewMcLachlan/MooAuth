using Asm.MooAuth.Modules.DataSources.Models.StaticList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.StaticList;

internal class Update : AbstractValidator<Commands.StaticList.Update>
{
    public Update()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateStaticListDataSource>());
    }
}

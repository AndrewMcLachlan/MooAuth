using Asm.AspNetCore.Validators;
using FluentValidation;

namespace Asm.MooAuth.Modules.Applications.Validators.Permissions;

internal class Update : AbstractValidator<Commands.Permissions.Update>
{
    public Update()
    {
        RuleFor(x => x.Permission).SetValidator(new DescribedValidator<Models.CreatePermission>());
    }
}

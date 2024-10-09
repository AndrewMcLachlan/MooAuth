using Asm.AspNetCore.Validators;
using FluentValidation;

namespace Asm.MooAuth.Modules.Applications.Validators.Permissions;

internal class Create : AbstractValidator<Commands.Permissions.Create>
{
    public Create()
    {
        RuleFor(x => x.Permission).SetValidator(new DescribedValidator<Models.CreatePermission>());
    }
}

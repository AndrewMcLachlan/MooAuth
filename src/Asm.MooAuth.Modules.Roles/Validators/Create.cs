using Asm.AspNetCore.Validators;
using FluentValidation;

namespace Asm.MooAuth.Modules.Roles.Validators;

internal class Create : AbstractValidator<Commands.Roles.Create>
{
    public Create()
    {
        RuleFor(x => x.Role).SetValidator(new DescribedValidator<Models.CreateRole>());
    }
}

using Asm.AspNetCore.Validators;
using FluentValidation;

namespace Asm.MooAuth.Modules.Roles.Validators;
internal class Update : AbstractValidator<Commands.Roles.Update>
{
    public Update()
    {
        RuleFor(x => x.Role).SetValidator(new DescribedValidator<Models.CreateRole>());
    }
}

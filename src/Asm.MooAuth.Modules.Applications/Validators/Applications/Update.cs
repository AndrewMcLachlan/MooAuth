using Asm.AspNetCore.Validators;
using FluentValidation;

namespace Asm.MooAuth.Modules.Applications.Validators.Applications;
internal class Update : AbstractValidator<Commands.Applications.Update>
{
    public Update()
    {
        RuleFor(x => x.Application).SetValidator(new DescribedValidator<Models.CreateApplication>());
    }
}

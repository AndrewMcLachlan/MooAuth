using Asm.AspNetCore.Validators;
using FluentValidation;

namespace Asm.MooAuth.Modules.Applications.Validators.Applications;

internal class Create : AbstractValidator<Commands.Applications.Create>
{
    public Create()
    {
        RuleFor(x => x.Application).SetValidator(new DescribedValidator<Models.CreateApplication>());
    }
}

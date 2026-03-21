using Asm.MooAuth.Modules.DataSources.Models;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators;

internal class DataSourceValidator<T> : AbstractValidator<T> where T : IDataSourceRequest
{
    public DataSourceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must be 50 characters or less");

        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required")
            .MaximumLength(50).WithMessage("Key must be 50 characters or less")
            .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Key can only contain letters, numbers, underscores, and hyphens");

        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Description must be 255 characters or less")
            .When(x => x.Description != null);
    }
}

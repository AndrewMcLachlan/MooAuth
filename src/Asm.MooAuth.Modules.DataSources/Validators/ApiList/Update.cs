using Asm.MooAuth.Modules.DataSources.Models.ApiList;
using FluentValidation;

namespace Asm.MooAuth.Modules.DataSources.Validators.ApiList;

internal class Update : AbstractValidator<Commands.ApiList.Update>
{
    public Update()
    {
        RuleFor(x => x.DataSource).SetValidator(new DataSourceValidator<CreateApiListDataSource>());

        RuleFor(x => x.DataSource.Config.Endpoint)
            .NotEmpty().WithMessage("Endpoint is required");

        RuleFor(x => x.DataSource.Config.TokenEndpoint)
            .NotEmpty().WithMessage("Token endpoint is required for OAuth authentication")
            .When(x => x.DataSource.Config.AuthType == ApiAuthType.OAuthClientCredentials);

        RuleFor(x => x.DataSource.Config.ClientId)
            .NotEmpty().WithMessage("Client ID is required for OAuth authentication")
            .When(x => x.DataSource.Config.AuthType == ApiAuthType.OAuthClientCredentials);
    }
}

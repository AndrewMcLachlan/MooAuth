# MooAuth

![Build](https://github.com/andrewmclachlan/MooAuth/actions/workflows/build.yml/badge.svg)

## Getting Started

### Prerequisites

* Node JS
* .NET SDK 10.x

### Authentication & Authorisation

The initial version of MooAuth is coupled to Azure Entra for its own authentication. Future version will fix this to allow any OAuth 2.0 provider.

Set up an application registration in Azure Entra.

1. Add a Redirect URI for SPA: https://localhost:3006
2. Expose an API with Application ID URI: `api://moobank.mclachlan.family`
3. Add a scope: `api.read`

Create an appsettings file `appsettings.Development.json` file in the `Asm.MooAuth.Web.Api` project and add the following configuration:

```json
"MooAuth": {
  "OAuth": {
    "TenantId": "{Your Entra Tenant ID}",
    "Domain": "https://login.microsoftonline.com/",
    "ClientId": "{Your App Registration Client ID}",
    "Audience": "{Your App Registration Audience}",
    "ValidateAudience": true
  }
}
```

### Set up a Secret Manager

A secret manager stores secrets generated when adding connectors and data sources. In this version only Azure Key Vault is supported.

The account that runs MooAuth requires both read and write access to this Key Vault.

In your appsettings file, add the following:

```json
"MooAuth": {
    ...
    "SecretManager": {
        "Uri": "{Key Vault URI}"
    }
}
using Microsoft.AspNetCore.Http;

namespace Asm.MooAuth.Modules.Users.Queries;

internal record Get() : IQuery<Models.User>;

internal class GetHandler(IHttpContextAccessor httpContextAccessor) : IQueryHandler<Get, Models.User>
{
    public ValueTask<Models.User> Handle(Get query, CancellationToken cancellationToken)
    {
        var claims = httpContextAccessor.HttpContext?.User.Claims;

        return claims == null
            ? throw new NotFoundException("User not found")
            : ValueTask.FromResult(new Models.User
            {
                Id = claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value,
                EmailAddress = claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value,
                FirstName = claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value,
                LastName = claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value,
            });
    }
}

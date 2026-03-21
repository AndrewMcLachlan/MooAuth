using System.ComponentModel;

namespace Asm.MooAuth.Modules.Users.Models;

[DisplayName("User")]
public record User
{
    public required object Id { get; init; }
    public required string EmailAddress { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}

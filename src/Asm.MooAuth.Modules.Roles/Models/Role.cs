namespace Asm.MooAuth.Modules.Roles.Models;
public record Role
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? LogoUrl { get; init; }

    public IEnumerable<Permission> Permissions { get; init; } = [];
}

public static class RoleExtensions
{
    public static Role ToModel(this Domain.Entities.Roles.Role role) => new()
    {
        Id = role.Id,
        Name = role.Name,
        Description = role.Description,
        Permissions = role.Permissions.Select(p => p.ToModel())
    };

    public static IQueryable<Role> ToModel(this IQueryable<Domain.Entities.Roles.Role> roles) =>
        roles.Select(a => a.ToModel());
}

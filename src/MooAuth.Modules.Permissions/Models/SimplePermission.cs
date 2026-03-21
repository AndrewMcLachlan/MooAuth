namespace Asm.MooAuth.Modules.Permissions.Models;
public record SimplePermission
{
    public int Id { get; init; }

    public required string Name { get; init; }
}

internal static class PermissionExtensions
{
    public static SimplePermission ToModel(Domain.Entities.Permissions.Permission permission) => new()
    {
        Id = permission.Id,
        Name = permission.ToString()
    };

    public static IQueryable<SimplePermission> ToModel(IQueryable<Domain.Entities.Permissions.Permission> permissions) =>
        permissions.Select(p => p.ToModel());
}

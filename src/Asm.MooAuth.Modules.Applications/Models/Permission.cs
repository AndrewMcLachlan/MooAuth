namespace Asm.MooAuth.Modules.Applications.Models;
public record Permission : SimplePermission
{
    public string? Description { get; init; }
}

public static class PermissionExtensions
{
    public static Permission ToModel(this Domain.Entities.Permissions.Permission permission) => new()
    {
        Id = permission.Id,
        Name = permission.Name,
        Description = permission.Description
    };

    public static IQueryable<Permission> ToModel(this IQueryable<Domain.Entities.Permissions.Permission> permissions) =>
        permissions.Select(p => p.ToModel());
}

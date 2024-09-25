namespace Asm.MooAuth.Modules.Applications.Models;
public record Permission
{
    public required int Id { get; init; }
    public required string Name { get; init; }
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

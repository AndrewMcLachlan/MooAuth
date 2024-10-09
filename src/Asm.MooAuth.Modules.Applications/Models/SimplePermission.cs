namespace Asm.MooAuth.Modules.Applications.Models;
public record SimplePermission : INamed
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}

internal static class SimplePermissionExtensions
{
    public static SimplePermission ToSimpleModel(this Domain.Entities.Permissions.Permission permission) => new()
    {
        Id = permission.Id,
        Name = permission.Name,
    };

    public static ICollection<SimplePermission> ToSimpleModel(this ICollection<Domain.Entities.Permissions.Permission> permissions) =>
        permissions.Select(p => p.ToSimpleModel()).ToList();
}

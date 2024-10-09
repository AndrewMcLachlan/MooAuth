namespace Asm.MooAuth.Modules.Applications.Models;

public record SimpleApplication : INamed
{
    public required int Id { get; init; }
    public required string Name { get; init; }

    public IEnumerable<SimplePermission> Permissions { get; init; } = [];
}

public static class SimpleApplicationExtensions
{
    public static SimpleApplication ToSimpleModel(this Domain.Entities.Applications.Application application) => new()
    {
        Id = application.Id,
        Name = application.Name,
        Permissions = application.Permissions.ToSimpleModel()
    };

    public static IQueryable<SimpleApplication> ToSimpleModel(this IQueryable<Domain.Entities.Applications.Application> applications) =>
        applications.Select(a => a.ToSimpleModel());
}

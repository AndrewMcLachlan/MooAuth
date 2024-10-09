namespace Asm.MooAuth.Modules.Applications.Models;
public record Application : Described
{
    public required int Id { get; init; }
    public string? LogoUrl { get; init; }

    public IEnumerable<Permission> Permissions { get; init; } = [];
}

public static class ApplicationExtensions
{
    public static Application ToModel(this Domain.Entities.Applications.Application application) => new()
    {
        Id = application.Id,
        Name = application.Name,
        Description = application.Description,
        LogoUrl = application.LogoUrl,
        Permissions = application.Permissions.Select(p => p.ToModel())
    };

    public static IQueryable<Application> ToModel(this IQueryable<Domain.Entities.Applications.Application> applications) =>
        applications.Select(a => a.ToModel());
}

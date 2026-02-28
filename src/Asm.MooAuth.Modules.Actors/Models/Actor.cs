namespace Asm.MooAuth.Modules.Actors.Models;

public record Actor
{
    public required int Id { get; init; }

    public required string ExternalId { get; init; }

    public required MooAuth.Models.ActorType ActorType { get; init; }

    public required int ConnectorId { get; init; }

    public IEnumerable<ActorRoleAssignment> RoleAssignments { get; init; } = [];
}

public static class ActorExtensions
{
    public static Actor ToModel(this Domain.Entities.Actors.Actor actor) => new()
    {
        Id = actor.Id,
        ExternalId = actor.ExternalId,
        ActorType = actor.ActorType,
        ConnectorId = actor.ConnectorId,
        RoleAssignments = actor.RoleResources.GroupBy(rr => rr.RoleId).Select(g => new ActorRoleAssignment
        {
            RoleId = g.Key,
            RoleName = g.First().Role?.Name ?? "",
            Resources = g.Select(rr => new ActorRoleResource
            {
                DataSourceId = rr.DataSourceId,
                DataSourceName = rr.DataSource?.Name,
                DataSourceKey = rr.DataSource?.Key,
                ResourceValue = rr.ResourceValue,
            }).ToList(),
        }),
    };
}

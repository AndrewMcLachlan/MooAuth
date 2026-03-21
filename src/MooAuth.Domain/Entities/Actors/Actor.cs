using Asm.MooAuth.Domain.Entities.Connectors;

namespace Asm.MooAuth.Domain.Entities.Actors;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class Actor([DisallowNull] int id) : KeyedEntity<int>(id)
{
    public Actor() : this(default) { }

    [MaxLength(100)]
    [Required]
    public string ExternalId { get; set; } = default!;

    public int ConnectorId { get; set; }

    [Column("ActorTypeId")]
    public Models.ActorType ActorType { get; set; }

    public Connector Connector { get; set; } = default!;

    public ICollection<ActorRoleResource> RoleResources { get; set; } = [];
}

using Asm.MooAuth.Domain.Entities.DataSources;
using Asm.MooAuth.Domain.Entities.Roles;

namespace Asm.MooAuth.Domain.Entities.Actors;

[PrimaryKey(nameof(Id))]
public class ActorRoleResource([DisallowNull] int id) : KeyedEntity<int>(id)
{
    public ActorRoleResource() : this(default) { }

    public int ActorId { get; set; }

    public int RoleId { get; set; }

    public int? DataSourceId { get; set; }

    public string? ResourceValue { get; set; }

    public Actor Actor { get; set; } = default!;

    public Role Role { get; set; } = default!;

    public DataSource? DataSource { get; set; }
}

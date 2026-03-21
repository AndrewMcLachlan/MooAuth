using Asm.MooAuth.Domain.Entities.Permissions;

namespace Asm.MooAuth.Domain.Entities.Roles;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class Role([DisallowNull] int id) : DescribedEntity(id)
{
    public Role() : this(default) { }

    public ICollection<Permission> Permissions { get; set; } = [];
}

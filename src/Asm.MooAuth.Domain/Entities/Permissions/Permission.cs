using Asm.MooAuth.Domain.Entities.Applications;
using Asm.MooAuth.Domain.Entities.Roles;

namespace Asm.MooAuth.Domain.Entities.Permissions;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class Permission([DisallowNull] int id) : NamedEntity(id)
{
    public Permission() : this(default) { }

    public required int ApplicationId { get; set; }

    public Application Application { get; set; } = null!;

    public ICollection<Role> Roles { get; set; } = [];

    public override string ToString() => $"{Application?.Name}.{Name}";
}

using Asm.MooAuth.Domain.Entities.Permissions;

namespace Asm.MooAuth.Domain.Entities.Applications;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class Application([DisallowNull] int id) : DescribedEntity(id)
{
    public Application() : this(default) { }

    public ICollection<Permission> Permissions { get; set; } = [];

    public string? LogoUrl { get; set; }
}

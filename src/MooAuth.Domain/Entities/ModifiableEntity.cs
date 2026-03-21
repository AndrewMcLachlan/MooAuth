namespace Asm.MooAuth.Domain.Entities;

public abstract class ModifiableEntity(int id) : KeyedEntity<int>(id)
{
    protected ModifiableEntity() : this(default) { }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public DateTime Modified { get; set; } = DateTime.UtcNow;
}

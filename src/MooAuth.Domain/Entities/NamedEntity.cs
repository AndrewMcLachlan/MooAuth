namespace Asm.MooAuth.Domain.Entities;

public abstract class NamedEntity(int id) : ModifiableEntity(id)
{
    protected NamedEntity() : this(default) { }

    [MaxLength(50)]
    [Required]
    [AllowNull]
    public string Name { get; set; }
}

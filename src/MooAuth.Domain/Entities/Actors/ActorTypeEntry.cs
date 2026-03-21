namespace Asm.MooAuth.Domain.Entities.Actors;

[PrimaryKey(nameof(Id))]
public class ActorTypeEntry([DisallowNull] int id) : KeyedEntity<int>(id)
{
    public ActorTypeEntry() : this(default) { }

    [MaxLength(50)]
    [Required]
    public string Name { get; set; } = default!;
}

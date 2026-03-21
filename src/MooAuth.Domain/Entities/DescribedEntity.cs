namespace Asm.MooAuth.Domain.Entities;
public class DescribedEntity(int id) : NamedEntity(id)
{
    [MaxLength(255)]
    public string? Description { get; set; }
}

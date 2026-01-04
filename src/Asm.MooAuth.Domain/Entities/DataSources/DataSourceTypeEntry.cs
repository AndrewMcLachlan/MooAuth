namespace Asm.MooAuth.Domain.Entities.DataSources;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class DataSourceTypeEntry(int id) : KeyedEntity<int>(id)
{
    [MaxLength(50)]
    public required string Name { get; set; }
}

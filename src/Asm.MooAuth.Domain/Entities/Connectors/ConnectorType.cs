namespace Asm.MooAuth.Domain.Entities.Connectors;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class ConnectorType(int id) : KeyedEntity<int>(id)
{
    [MaxLength(50)]
    public required string Name { get; set; }

    public Uri? LogoUrl { get; init; }
}

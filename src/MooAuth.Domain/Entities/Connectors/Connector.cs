
using System.Text.Json;
using Asm.MooAuth.Models;

namespace Asm.MooAuth.Domain.Entities.Connectors;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class Connector([DisallowNull] int id) : NamedEntity(id)
{
    public Connector() : this(default) { }

    [Column("ConnectorTypeId")]
    public Models.ConnectorType ConnectorType { get; set; }

    [MaxLength(50)]
    public string Slug { get; set; } = default!;

    [MaxLength(100)]
    public string ClientId { get; set; } = default!;

    [MaxLength(8000)]
    public string Config { get; private set; } = "{}";

    public void SetConfig<T>(T config)
    {
        Config = JsonSerializer.Serialize(config);
    }
}

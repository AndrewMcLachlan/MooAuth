using System.Text.Json;

namespace Asm.MooAuth.Domain.Entities.DataSources;

[AggregateRoot]
[PrimaryKey(nameof(Id))]
public class DataSource([DisallowNull] int id) : DescribedEntity(id)
{
    public DataSource() : this(default) { }

    [Column("DataSourceTypeId")]
    public Models.DataSourceType DataSourceType { get; set; }

    [MaxLength(50)]
    [Required]
    public string Key { get; set; } = default!;

    [MaxLength(4000)]
    public string? Config { get; private set; }

    public ICollection<DataSourceValue> Values { get; set; } = [];

    public void SetConfig<T>(T config)
    {
        Config = JsonSerializer.Serialize(config);
    }

    public T? GetConfig<T>() where T : class
    {
        if (String.IsNullOrEmpty(Config)) return null;
        return JsonSerializer.Deserialize<T>(Config);
    }
}

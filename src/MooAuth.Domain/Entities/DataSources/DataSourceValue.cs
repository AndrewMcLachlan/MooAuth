namespace Asm.MooAuth.Domain.Entities.DataSources;

[PrimaryKey(nameof(Id))]
public class DataSourceValue([DisallowNull] int id) : KeyedEntity<int>(id)
{
    public DataSourceValue() : this(default) { }

    public int DataSourceId { get; set; }

    [MaxLength(100)]
    [Required]
    public string Key { get; set; } = default!;

    [MaxLength(255)]
    [Required]
    public string DisplayValue { get; set; } = default!;

    public int SortOrder { get; set; }

    public DataSource DataSource { get; set; } = default!;
}

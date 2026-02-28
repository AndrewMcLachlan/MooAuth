namespace Asm.MooAuth.Models;

public static class DataSourceTypeExtensions
{
    public static string ToDisplayName(this DataSourceType type) => type switch
    {
        DataSourceType.FreeText => "Free Text",
        DataSourceType.Checkbox => "Checkbox",
        DataSourceType.PickList => "Pick List",
        DataSourceType.ApiPickList => "Pick List (API)",
        _ => type.ToString()
    };
}

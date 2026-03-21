namespace Asm.MooAuth.Modules.DataSources.Models;

public interface IDataSourceRequest
{
    string Name { get; }

    string? Description { get; }

    string Key { get; }
}

namespace Asm.MooAuth;

public interface IConnectorFactory
{
    Task<IConnector> CreateAsync(CancellationToken cancellationToken = default);
}

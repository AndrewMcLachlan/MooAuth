namespace Asm.MooAuth.Domain.Entities.Applications;

public interface IApplicationRepository : IWritableRepository<Application,int>, IDeletableRepository<Application, int>
{
}

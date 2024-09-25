using System.Reflection;
using Asm.MooAuth.Domain.Entities.Applications;
using Asm.MooAuth.Domain.Entities.Permissions;
using Asm.MooAuth.Domain.Entities.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Asm.MooAuth.Infrastructure;

public partial class MooAuthContext : DomainDbContext, IReadOnlyDbContext
{
    private static readonly List<Assembly> Assemblies = [];

    public MooAuthContext(IMediator mediator) : base(mediator)
    {
    }

    public MooAuthContext(DbContextOptions<MooAuthContext> options, IMediator mediator) : base(options, mediator)
    {
    }

    public static void RegisterAssembly(Assembly assembly) => Assemblies.Add(assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.ClrType.Name);
        }

        modelBuilder.Entity<Application>();
        modelBuilder.Entity<Permission>();
        modelBuilder.Entity<Role>();

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        Assemblies.ForEach(a => modelBuilder.ApplyConfigurationsFromAssembly(a));
    }
}

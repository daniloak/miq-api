using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Omini.Miq.Domain.Authentication;
using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Infrastructure;

public class MiqContext : DbContext
{
     public MiqContext(DbContextOptions<MiqContext> options)
        : base(options)
    {
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }

    public DbSet<MiqUser> MiqUsers { get; set; }
    public DbSet<Promissory> Promissories { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
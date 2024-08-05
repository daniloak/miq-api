using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Omini.Miq.Domain.Authentication;
using Omini.Miq.Domain.Sales;
using Omini.Miq.Infrastructure.Extensions;

namespace Omini.Miq.Infrastructure;

public sealed class MiqContext : DbContext
{
    public MiqContext(DbContextOptions<MiqContext> options)
       : base(options)
    {
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Promissory> Promissories { get; set; }
    public DbSet<PromissoryItem> PromissoryItems { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(MiqContext).Assembly);

        builder.EnableSoftDeleteQuery();

        builder.ApplyDefaultRules();
        builder.Seed();

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
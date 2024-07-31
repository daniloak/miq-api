using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Omini.Miq.Domain.Common;
using Omini.Miq.Domain.Exceptions;
using Omini.Miq.Shared.Services.Security;

namespace Omini.Miq.Infrastructure.Interceptors;

public sealed class SoftDeletableInterceptor : SaveChangesInterceptor
{
    private readonly IClaimsService _claimsService;
    public SoftDeletableInterceptor(IClaimsService claimsService)
    {
        _claimsService = claimsService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(
                eventData, result, cancellationToken);
        }

        UpdateDeleted(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateDeleted(DbContextEventData eventData)
    {
        var miqUserId = _claimsService.MiqUserId;
        if (miqUserId is null)
        {
            throw new InvalidUserException();
        }

        var softDeletables =
                    eventData
                        .Context
                        .ChangeTracker.Entries()
                        .Where(e => typeof(ISoftDeletable).IsAssignableFrom(e.Entity.GetType()) && e.State == EntityState.Deleted);

        foreach (var softDeletable in softDeletables)
        {
            softDeletable.State = EntityState.Modified;
            var auditableEntity = softDeletable.Entity as ISoftDeletable;

            auditableEntity.DeletedBy = miqUserId.Value;
            auditableEntity.DeletedOn = DateTime.UtcNow;
            auditableEntity.IsDeleted = true;
        }
    }
}
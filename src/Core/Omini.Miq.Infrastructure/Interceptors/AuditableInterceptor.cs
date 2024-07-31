using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Omini.Miq.Domain.Common;
using Omini.Miq.Domain.Exceptions;
using Omini.Miq.Shared.Services.Security;

namespace Omini.Miq.Infrastructure.Interceptors;

public sealed class AuditableInterceptor : SaveChangesInterceptor
{
    private readonly IClaimsService _claimsService;
    public AuditableInterceptor(IClaimsService claimsService)
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

        UpdateAdded(eventData);
        UpdateModified(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAdded(DbContextEventData eventData)
    {
        var miqUserId = _claimsService.MiqUserId;
        if (miqUserId is null)
        {
            throw new InvalidUserException();
        }

        var auditables =
                    eventData
                        .Context!
                        .ChangeTracker.Entries()
                        .Where(e => typeof(IAuditable).IsAssignableFrom(e.Entity.GetType()) && e.State == EntityState.Added);

        foreach (var auditable in auditables)
        {
            var auditableEntity = auditable.Entity as IAuditable;
            auditableEntity!.CreatedBy = miqUserId.Value;
            auditableEntity.CreatedOn = DateTime.UtcNow;
        }
    }

    private void UpdateModified(DbContextEventData eventData)
    {
        var miqUserId = _claimsService.MiqUserId;
        if (miqUserId is null)
        {
            throw new InvalidUserException();
        }

        var auditables =
                    eventData
                        .Context!
                        .ChangeTracker.Entries()
                        .Where(e => typeof(IAuditable).IsAssignableFrom(e.Entity.GetType()) && e.State == EntityState.Modified);

        foreach (var auditable in auditables)
        {
            var auditableEntity = auditable.Entity as IAuditable;
            auditableEntity!.UpdatedBy = miqUserId.Value;
            auditableEntity.UpdatedOn = DateTime.UtcNow;
        }
    }
}

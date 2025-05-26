using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors;
public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            { EntityState.Added, AddBehavior },
            { EntityState.Modified,UpdateBehavior }
        };

    private static void AddBehavior(DbContext dbContext, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.UtcNow;
        dbContext.Entry(auditEntity).Property(x => x.Updated).IsModified = false;

    }

    private static void UpdateBehavior(DbContext dbContext, IAuditEntity auditEntity)
    {
        auditEntity.Updated = DateTime.UtcNow;
        dbContext.Entry(auditEntity).Property(x => x.Created).IsModified = false;
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {

            if (entityEntry.Entity is not IAuditEntity auditEntity) continue;
            Behaviors[entityEntry.State](eventData.Context, auditEntity);
        }
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

}


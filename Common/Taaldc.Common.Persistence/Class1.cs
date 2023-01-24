

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeedWork;

namespace Taaldc.Common.Persistence;

public static class DbContextExtensions
{
    public static void DbAudit(this DbContext context, IAmCurrentUser _currentUser)
    {
        context.ChangeTracker.DetectChanges();

        foreach (EntityEntry entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is Enumeration
                || entry.Entity is ValueObject
                || entry.State == EntityState.Detached
                || entry.State == EntityState.Unchanged)
            {
                continue;
            }

            if (entry.Entity is IAuditable auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.AuditOnCreate(!string.IsNullOrEmpty(_currentUser?.Email)? _currentUser?.Email: String.Empty);
                        break;

                    case EntityState.Modified:
                        auditable.AuditOnUpdate(!string.IsNullOrEmpty(_currentUser?.Email) ? _currentUser?.Email : String.Empty, true);
                        break;

                    case EntityState.Deleted:
                        break;
                }
            }
        }
    }
}
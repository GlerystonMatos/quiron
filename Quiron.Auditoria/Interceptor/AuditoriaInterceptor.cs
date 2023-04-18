using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Quiron.Auditoria.Context;
using Quiron.Auditoria.Entities;

namespace Quiron.Auditoria.Interceptor
{
    public class AuditoriaInterceptor : ISaveChangesInterceptor
    {
        private readonly string _user;
        private readonly string _connectionString;
        private SaveChangesAudit _saveChangesAudit = new SaveChangesAudit();

        public AuditoriaInterceptor(string connectionString, string user)
        {
            _user = user;
            _connectionString = connectionString;
        }

        #region SavingChanges
        public async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context != null)
            {
                _saveChangesAudit = CreateAudit(eventData.Context, _user);

                using AuditoriaContext auditoriaContext = new AuditoriaContext(_connectionString);

                auditoriaContext.Add(_saveChangesAudit);
                await auditoriaContext.SaveChangesAsync();
            }

            return result;
        }

        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context != null)
            {
                _saveChangesAudit = CreateAudit(eventData.Context, _user);

                using AuditoriaContext auditoriaContext = new AuditoriaContext(_connectionString);
                auditoriaContext.Add(_saveChangesAudit);
                auditoriaContext.SaveChanges();
            }

            return result;
        }
        #endregion

        #region SavedChanges
        public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            using AuditoriaContext auditContext = new AuditoriaContext(_connectionString);

            auditContext.Attach(_saveChangesAudit);
            _saveChangesAudit.Succeeded = true;
            _saveChangesAudit.EndTime = DateTime.UtcNow;

            auditContext.SaveChanges();

            return result;
        }

        public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
            CancellationToken cancellationToken = default)
        {
            using AuditoriaContext auditContext = new AuditoriaContext(_connectionString);

            auditContext.Attach(_saveChangesAudit);
            _saveChangesAudit.Succeeded = true;
            _saveChangesAudit.EndTime = DateTime.UtcNow;

            await auditContext.SaveChangesAsync(cancellationToken);

            return result;
        }
        #endregion

        #region SaveChangesFailed
        public void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            using AuditoriaContext auditContext = new AuditoriaContext(_connectionString);

            auditContext.Attach(_saveChangesAudit);
            _saveChangesAudit.Succeeded = false;
            _saveChangesAudit.EndTime = DateTime.UtcNow;
            _saveChangesAudit.ErrorMessage = eventData.Exception.Message;

            auditContext.SaveChanges();
        }

        public async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            using AuditoriaContext auditContext = new AuditoriaContext(_connectionString);

            auditContext.Attach(_saveChangesAudit);
            _saveChangesAudit.Succeeded = false;
            _saveChangesAudit.EndTime = DateTime.UtcNow;

            if (eventData.Exception.InnerException != null)
            {
                _saveChangesAudit.ErrorMessage = eventData.Exception.InnerException.Message;
            }

            await auditContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region CreateAudit
        private static SaveChangesAudit CreateAudit(DbContext context, string user)
        {
            context.ChangeTracker.DetectChanges();

            SaveChangesAudit saveChangesAudit = new SaveChangesAudit
            {
                AuditId = Guid.NewGuid(),
                StartTime = DateTime.UtcNow
            };

            foreach (EntityEntry entityEntry in context.ChangeTracker.Entries())
            {
                string? auditMessage = entityEntry.State switch
                {
                    EntityState.Deleted => CreateDeletedMessage(entityEntry),
                    EntityState.Modified => CreateModifiedMessage(entityEntry),
                    EntityState.Added => CreateAddedMessage(entityEntry),
                    _ => null
                };

                if (auditMessage != null)
                {
                    saveChangesAudit.Entities.Add(new EntityAudit
                    {
                        AuditUser = user,
                        State = entityEntry.State,
                        AuditMessage = auditMessage,
                    }); ;
                }
            }

            return saveChangesAudit;

            string CreateAddedMessage(EntityEntry entry)
                => entry.Properties.Aggregate(
                    $"Inserting {entry.Metadata.DisplayName()} with ",
                    (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

            string CreateModifiedMessage(EntityEntry entry)
                => entry.Properties.Where(property => property.IsModified || property.Metadata.IsPrimaryKey()).Aggregate(
                    $"Updating {entry.Metadata.DisplayName()} with ",
                    (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

            string CreateDeletedMessage(EntityEntry entry)
                => entry.Properties.Where(property => property.Metadata.IsPrimaryKey()).Aggregate(
                    $"Deleting {entry.Metadata.DisplayName()} with ",
                    (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");
        }
        #endregion
    }
}
using Microsoft.EntityFrameworkCore;
using Quiron.Auditoria.Configuration;

namespace Quiron.Auditoria.Context
{
    public class AuditoriaContext : DbContext
    {
        private readonly string _connectionString;

        public AuditoriaContext(string connectionString)
            => _connectionString = connectionString;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EntityAuditConfig());
            modelBuilder.ApplyConfiguration(new SaveChangesAuditConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connectionString);
    }
}
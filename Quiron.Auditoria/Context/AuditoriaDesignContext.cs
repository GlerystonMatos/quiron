using Microsoft.EntityFrameworkCore.Design;
using Quiron.Auditoria.Context;

namespace Quiron.Data.Context
{
    public class AuditoriaDesignContext : IDesignTimeDbContextFactory<AuditoriaContext>
    {
        public AuditoriaContext CreateDbContext(string[] args)
            => new AuditoriaContext("Data Source=10.0.0.131\\SQLEXPRESS;Initial Catalog=QuironAuditoria;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False");
    }
}
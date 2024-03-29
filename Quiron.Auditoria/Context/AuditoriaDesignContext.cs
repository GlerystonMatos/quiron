﻿using Microsoft.EntityFrameworkCore.Design;

namespace Quiron.Auditoria.Context
{
    public class AuditoriaDesignContext : IDesignTimeDbContextFactory<AuditoriaContext>
    {
        public AuditoriaContext CreateDbContext(string[] args)
            => new AuditoriaContext("Data Source=10.0.0.131;Initial Catalog=QuironAuditoria;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False");
    }
}
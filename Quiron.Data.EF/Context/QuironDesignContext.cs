using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Quiron.Service.Services;

namespace Quiron.Data.EF.Context
{
    public class DesignQuironContext : IDesignTimeDbContextFactory<QuironContext>
    {
        public QuironContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<QuironContext> builder = new DbContextOptionsBuilder<QuironContext>();
            builder.UseSqlServer("Data Source=10.0.0.131\\SQLEXPRESS;Initial Catalog=Quiron;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False");
            return new QuironContext(builder.Options, new TenantService(), null);
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Quiron.NUnitTest.Utilitarios
{
    public class DBInitializer
    {
        public void Seed(DbContext context)
        {
            context.Database.EnsureDeletedAsync();
            context.Database.EnsureCreatedAsync();
            context.SaveChangesAsync();
        }
    }
}
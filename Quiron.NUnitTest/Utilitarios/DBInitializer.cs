using Microsoft.EntityFrameworkCore;

namespace Quiron.NUnitTest.Utilitarios
{
    public class DBInitializer
    {
        public void Seed(DbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
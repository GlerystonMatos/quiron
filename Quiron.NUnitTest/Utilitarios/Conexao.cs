using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quiron.Data.EF.Context;
using Quiron.Service.Services;

namespace Quiron.NUnitTest.Utilitarios
{
    public class Conexao
    {
        private static QuironContext? _quironContext;

        public static QuironContext GetContext()
        {
            if (_quironContext == null)
            {
                DbContextOptions<QuironContext> optionsDados = new DbContextOptionsBuilder<QuironContext>().Options;

                LoggerFactory loggerFactory = new LoggerFactory();
                ILogger<QuironContext> logger = loggerFactory.CreateLogger<QuironContext>();

                TenantService tenantService = Tenant.GetTenant();
                _quironContext = new QuironContext(optionsDados, tenantService, logger);

                DBInitializer quironDBInitializer = new DBInitializer();
                quironDBInitializer.Seed(_quironContext);
            }

            return _quironContext;
        }
    }
}
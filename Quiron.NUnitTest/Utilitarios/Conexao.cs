using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quiron.Data.Context;
using Quiron.Domain.Tenant;
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
                TenantConfiguration tenantConfiguration = new TenantConfiguration();
                tenantConfiguration.Name = "NUnitTest";
                tenantConfiguration.ConnectionStringDados = "Data Source=10.0.0.131\\SQLEXPRESS;Initial Catalog=QuironNUnitTest;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False";
                tenantConfiguration.ConnectionStringAuditoria = "Data Source=10.0.0.131\\SQLEXPRESS;Initial Catalog=QuironAuditoriaNUnitTest;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False";

                DbContextOptions<QuironContext> optionsDados = new DbContextOptionsBuilder<QuironContext>().Options;

                TenantService tenantService = new TenantService();
                tenantService.Set(tenantConfiguration);
                tenantService.SetUser(tenantConfiguration.Name);

                LoggerFactory loggerFactory = new LoggerFactory();
                ILogger<QuironContext> logger = loggerFactory.CreateLogger<QuironContext>();

                _quironContext = new QuironContext(optionsDados, tenantService, logger);

                DBInitializer quironDBInitializer = new DBInitializer();
                quironDBInitializer.Seed(_quironContext);
            }

            return _quironContext;
        }
    }
}
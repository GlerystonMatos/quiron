using Quiron.Domain.Tenant;
using Quiron.Service.Services;

namespace Quiron.NUnitTest.Utilitarios
{
    public class Tenant
    {
        private static TenantService _tenantService = new TenantService();

        public static TenantService GetTenant()
        {
            if (_tenantService == null)
            {
                TenantConfiguration tenantConfiguration = new TenantConfiguration();
                tenantConfiguration.Name = "NUnitTest";
                tenantConfiguration.ConnectionStringDados = "Data Source=10.0.0.131\\SQLEXPRESS;Initial Catalog=QuironNUnitTest;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False";
                tenantConfiguration.ConnectionStringAuditoria = "Data Source=10.0.0.131\\SQLEXPRESS;Initial Catalog=QuironAuditoriaNUnitTest;Persist Security Info=True;User ID=sa;Password=1234;Encrypt=False";

                _tenantService = new TenantService();
                _tenantService.Set(tenantConfiguration);
                _tenantService.SetUser(tenantConfiguration.Name);
            }

            return _tenantService;
        }
    }
}
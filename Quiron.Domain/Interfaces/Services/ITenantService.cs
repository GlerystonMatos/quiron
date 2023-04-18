using Quiron.Domain.Tenant;

namespace Quiron.Domain.Interfaces.Services
{
    public interface ITenantService
    {
        TenantConfiguration Get();

        void Set(TenantConfiguration tenant);

        string GetUser();

        void SetUser(string user);
    }
}
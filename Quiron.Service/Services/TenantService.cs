using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;

namespace Quiron.Service.Services
{
    public class TenantService : ITenantService
    {
        private string _user;
        private TenantConfiguration _tenant;

        public TenantConfiguration Get()
            => _tenant;

        public void Set(TenantConfiguration tenant)
            => _tenant = tenant;

        public string GetUser()
            => _user;

        public void SetUser(string user)
            => _user = user;
    }
}
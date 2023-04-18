namespace Quiron.Domain.Tenant
{
    public class TenantConfiguration
    {
        public string Name { get; set; }

        public string ConnectionStringDados { get; set; }

        public string ConnectionStringAuditoria { get; set; }
    }
}
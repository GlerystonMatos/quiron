using Microsoft.EntityFrameworkCore;

namespace Quiron.Auditoria.Entities
{
    public class EntityAudit
    {
        public EntityAudit()
        {
            AuditUser = "";
            AuditMessage = "";
            Id = Guid.NewGuid();
            SaveChangesAudit = new SaveChangesAudit();
        }

        public Guid Id { get; set; }

        public string AuditUser { get; set; }

        public EntityState State { get; set; }

        public string AuditMessage { get; set; }

        public Guid SaveChangesAuditId { get; set; }

        public SaveChangesAudit SaveChangesAudit { get; set; }
    }
}
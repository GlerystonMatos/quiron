namespace Quiron.Auditoria.Entities
{
    public class SaveChangesAudit
    {
        public SaveChangesAudit()
        {
            ErrorMessage = "";
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid AuditId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }

        public ICollection<EntityAudit> Entities { get; } = new List<EntityAudit>();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiron.Domain.Entities;

namespace Quiron.Data.EF.Configuration
{
    public class CidadeConfig : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Nome).IsRequired();
            builder.Property(c => c.IdEstado).IsRequired();
            builder.HasOne(c => c.Estado).WithMany(e => e.Cidades).HasForeignKey(c => c.IdEstado).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
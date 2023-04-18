using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiron.Domain.Entities;

namespace Quiron.Data.Configuration
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).IsRequired();
            builder.Property(u => u.Nome).IsRequired();
            builder.Property(u => u.Login).IsRequired();
            builder.Property(u => u.Senha).IsRequired();
        }
    }
}
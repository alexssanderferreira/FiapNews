using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configuracoes
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(u => u.Id);
            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.EnderecoEmail)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar(max)");
            });
        }
    }
}

using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configuracoes
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentario");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().HasColumnType("uniqueidentifier");
            builder.Property(c => c.Texto).IsRequired().HasColumnType("varchar(1000)");
            builder.Property(c => c.DataCriacao).IsRequired().HasColumnType("datetime");
            builder.Property(c => c.EstadoValidacao).IsRequired().HasColumnType("int");
            builder.Property(c => c.DataValidacao).HasColumnType("datetime");
            builder.Property(c => c.MotivoRejeicao).HasColumnType("varchar(500)");
            builder.Property("NoticiaId").IsRequired().HasColumnType("uniqueidentifier");
            builder.Property("UsuarioId").IsRequired().HasColumnType("uniqueidentifier");
            builder.Property("ModeradorResponsavelId").HasColumnType("uniqueidentifier");
        }
    }
}

using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configuracoes
{
    public class NoticiaConfiguration : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.ToTable("Noticia");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.NoticiasRelacionadas).WithMany();
        }
    }


}

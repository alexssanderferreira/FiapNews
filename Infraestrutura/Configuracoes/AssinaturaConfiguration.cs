using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configuracoes
{
    public class AssinaturaConfiguration : IEntityTypeConfiguration<Assinatura>
    {
        public void Configure(EntityTypeBuilder<Assinatura> builder)
        {
            builder.ToTable("Assinatura");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Preco).HasPrecision(18, 2);
        }
    }

}

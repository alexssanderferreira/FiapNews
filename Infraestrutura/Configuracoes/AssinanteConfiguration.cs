using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configuracoes
{
    public class AssinanteConfiguration : IEntityTypeConfiguration<Assinante>
    {
        public void Configure(EntityTypeBuilder<Assinante> builder)
        {
            builder.ToTable("Assinante");
        }
    }


}

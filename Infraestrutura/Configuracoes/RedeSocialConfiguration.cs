using Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Configuracoes
{
    public class RedeSocialConfiguration : IEntityTypeConfiguration<RedeSocial>
    {
        public void Configure(EntityTypeBuilder<RedeSocial> builder)
        {
            builder.ToTable("RedesSociais");
            builder.HasKey(u => u.Id);
            
        }
    }
}

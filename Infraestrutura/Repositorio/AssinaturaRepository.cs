using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class AssinaturaRepository : RepositoryBase<Assinatura>, IAssinaturaRepository
    {
        public AssinaturaRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

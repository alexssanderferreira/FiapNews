using Aplicacao.Contratos.Persistencia;
using Dominio.ObjetosDeValor;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class RedeSocialRepository : RepositoryBase<RedeSocial>, IRedeSocialRepository
    {
        public RedeSocialRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

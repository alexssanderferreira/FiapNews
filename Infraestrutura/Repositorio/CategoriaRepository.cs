using Aplicacao.Contratos.Persistencia;
using Dominio.ObjetosDeValor;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

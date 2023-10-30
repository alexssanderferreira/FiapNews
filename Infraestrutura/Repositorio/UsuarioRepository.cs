using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

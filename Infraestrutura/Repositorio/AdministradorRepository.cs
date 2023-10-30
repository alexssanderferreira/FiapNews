using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio
{
    public class AdministradorRepository : RepositoryBase<Administrador>, IAdministradorRepository
    {
        public AdministradorRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
    }
}

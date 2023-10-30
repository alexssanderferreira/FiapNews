using Dominio.Entidades;

namespace Aplicacao.Contratos.Persistencia
{
    public interface IAssinanteRepository : IRepositoryBase<Assinante>
    {
        Task<IReadOnlyList<Assinante>> ObterAssinantes();
        Task<Assinante> ObterAssinantePorId(Guid id);
    }

}

using Dominio.Entidades;

namespace Aplicacao.Contratos.Persistencia
{
    public interface IAutorRepository : IRepositoryBase<Autor>
    {
        Task<IReadOnlyList<Autor>> ObterAutores();
        Task<Autor> ObterAutorPorId(Guid id);
    }

}

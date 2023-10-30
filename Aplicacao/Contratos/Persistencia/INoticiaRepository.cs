using Dominio.Entidades;
using Dominio.ObjetosDeValor;

namespace Aplicacao.Contratos.Persistencia
{
    public interface INoticiaRepository : IRepositoryBase<Noticia>
    {
        IEnumerable<Noticia> ObterNoticiaCategoria(Guid idCategoria);
    }

}

using Aplicacao.DTOs.Tag;
using Dominio.ObjetosDeValor;

namespace Aplicacao.Contratos.Persistencia
{
    public interface ITagRepository : IRepositoryBase<Tag>
    {
        Tag ObterPorTexto(string texto);
    }

}

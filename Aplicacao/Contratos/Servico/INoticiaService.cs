using Aplicacao.DTOs;
using Aplicacao.DTOs.Noticia;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico;

public interface INoticiaService : IServiceBase<NoticiaRetornoDto, NoticiaDto>
{
    IList<NoticiaRetornoDto> ObterNoticiaCategoria(Guid idCategoria);
}

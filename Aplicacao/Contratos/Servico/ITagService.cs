using Aplicacao.DTOs;
using Aplicacao.DTOs.Tag;
using Dominio.ObjetosDeValor;

namespace Aplicacao.Contratos.Servico;
public interface ITagService : IServiceBase<TagRetornoDto, TagDto>
{
    TagRetornoDto ObterPorTexto(string texto);
}

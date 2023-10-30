using Dominio.Entidades;
using Dominio.Enum;

namespace Aplicacao.Contratos.Persistencia;

public interface IComentarioRepository : IRepositoryBase<Comentario>
{
    Task<IEnumerable<Comentario>> GetComentario(EstadoValidacaoComentario estadoValidacao);
    Task<IEnumerable<Comentario>> GetComentarioPorNoticia(Guid idNoticia , EstadoValidacaoComentario  estadoValidacao);
}

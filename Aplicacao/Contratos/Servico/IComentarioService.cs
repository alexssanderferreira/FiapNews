using Aplicacao.DTOs.Comentario;

namespace Aplicacao.Contratos.Servico;

public interface IComentarioService
{
    Task Aprovar(Guid idComentario, Guid idAdministrador);
    Task Reprovar(Guid idComentario, Guid idAdministrador, string motivo);
    Task<IEnumerable<ComentarioRetornoDto>> GetAprovados();
    Task<IEnumerable<ComentarioRetornoDto>> GetReprovados();
    Task<IEnumerable<ComentarioRetornoDto>> GetPendentes();
    Task<IEnumerable<ComentarioRetornoDto>> GetAprovadosPorNoticia(Guid idNoticia);
    Task<IEnumerable<ComentarioRetornoDto>> GetReprovadosPorNoticia(Guid idNoticia);
    Task<IEnumerable<ComentarioRetornoDto>> GetPendentesPorNoticia(Guid idNoticia);
    Task<IReadOnlyList<ComentarioRetornoDto>> ObterTodosAsync();
    Task<ComentarioRetornoDto> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(ComentarioDto dto, Guid usuarioId, string role);
    Task DeletarAsync(Guid id);
}

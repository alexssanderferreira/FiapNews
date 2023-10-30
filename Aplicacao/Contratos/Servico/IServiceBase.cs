using Aplicacao.DTOs;

namespace Aplicacao.Contratos.Servico
{
    public interface IServiceBase<TDto, TDtoMod>
        where TDto : BaseDto
        where TDtoMod : class
    {
        Task<IReadOnlyList<TDto>> ObterTodosAsync();
        Task<TDto> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(TDtoMod dto);
        Task AlterarAsync(TDtoMod dto, Guid id);
        Task DeletarAsync(Guid id);
        IQueryable<TDto> ObterIQueryable();        
    }
}

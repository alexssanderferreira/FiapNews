using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao
{
    public class ServiceBase<TDto, TDtoMod, TEntity, TRepository> : IServiceBase<TDto, TDtoMod>
        where TDto : BaseDto
        where TDtoMod : class
        where TEntity : Base
        where TRepository : IRepositoryBase<TEntity>
    {
        protected TRepository Repository;
        protected readonly IMapper _mapper;
        protected List<string> _erros;
        public ServiceBase(TRepository repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
            _erros = new();
        }

        public virtual async Task AdicionarAsync(TDtoMod dto)
        {
            ValidarValores(dto);
            var entidade = await DefinirEntidadeInclusaoAsync(dto);
            await Repository.AdicionarAsync(entidade);
        }

        public virtual async Task AlterarAsync(TDtoMod dto, Guid id)
        {
            ValidarValores(dto);
            var entidade = await Repository.ObterPorIdAsync(id);
            entidade = await DefinirEntidadeAlteracao(entidade, dto);
            await Repository.AlterarAsync(entidade);
        }

        public async Task DeletarAsync(Guid id)
        {
            var entidade = await Repository.ObterPorIdAsync(id);
            ValidarDelecao(entidade);
            await Repository.DeletarAsync(entidade);
        }

        public IQueryable<TDto> ObterIQueryable()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TDto> ObterPorIdAsync(Guid id)
        {
            return _mapper.Map<TDto>(await Repository.ObterPorIdAsync(id));
        }

        public virtual async Task<IReadOnlyList<TDto>> ObterTodosAsync()
        {
            return _mapper.Map<IReadOnlyList<TDto>>(await Repository.ObterTodosAsync());
        }

        protected virtual void ValidarValores(TDtoMod dto)
        {
            throw new NotImplementedException();
        }
        protected virtual void ValidarDelecao(TEntity entidade)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task<TEntity> DefinirEntidadeInclusaoAsync(TDtoMod dto)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task<TEntity> DefinirEntidadeAlteracao(TEntity entidade, TDtoMod dto)
        {
            throw new NotImplementedException();
        }
    }
}

using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.RedeSocial;
using AutoMapper;
using Dominio.ObjetosDeValor;

namespace Aplicacao
{
    public class RedeSocialService : ServiceBase<RedeSocialRetornoDto,RedeSocialDto, RedeSocial, IRepositoryBase<RedeSocial>>, IRedeSocialService
    {
        public RedeSocialService(IRepositoryBase<RedeSocial> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override async Task<RedeSocial>  DefinirEntidadeInclusaoAsync(RedeSocialDto dto)
        {
            return new RedeSocial(dto.Nome, dto.Link);
        }

        protected override void ValidarValores(RedeSocialDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados da rede social.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                _erros.Add("Informe a descrição da rede social.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override async Task<RedeSocial> DefinirEntidadeAlteracao(RedeSocial entidade, RedeSocialDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Rede Social informada não encontrada.");

            entidade.DefinirNome(dto.Nome);
            entidade.DefinirLink(dto.Link);

            return entidade;
        }
        protected override void ValidarDelecao(RedeSocial entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Rede social informada não encontrada.");
        }
    }
}

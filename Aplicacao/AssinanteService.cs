using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Assinante;
using Aplicacao.DTOs.Usuario;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Aplicacao
{
    public class AssinanteService : ServiceBase<AssinanteRetornoDto, AssinanteDto, Assinante, IRepositoryBase<Assinante>>, IAssinanteService
    {
        private readonly IAssinanteRepository _repository;
        private readonly IAssinaturaRepository _assinaturaRepository;
        private readonly IUsuarioService<Assinante> _usuarioService;
        private readonly IHttpContextAccessor _accessor;

        public AssinanteService(
            IAssinanteRepository repository,
            IMapper mapper,
            IUsuarioService<Assinante> usuarioService,
            IHttpContextAccessor accessor,
            IAssinaturaRepository assinaturaRepository) : base(repository, mapper)
        {
            _repository = repository;
            _usuarioService = usuarioService;
            _accessor = accessor;
            _assinaturaRepository = assinaturaRepository;
        }

        public override async Task AlterarAsync(AssinanteDto dto, Guid id)
        {
            ValidarValores(dto);

            var entidade = await Repository.ObterPorIdAsync(id);
            entidade = await DefinirEntidadeAlteracao(entidade, dto);
            await Repository.AlterarAsync(entidade);
        }

        protected override async Task<Assinante> DefinirEntidadeInclusaoAsync(AssinanteDto dto)
        {
            var usuario = Repository.ObterIQueryable().FirstOrDefault(x => x.Login == dto.Login);

            //if (usuario != null)
            //    throw new Exception("Login informado não disponível.");

            var assinante = new Assinante(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto, null);
            return assinante;
        }

        protected override void ValidarValores(AssinanteDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                _erros.Add("Informe o nome do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Login))
                _erros.Add("Informe o login do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                _erros.Add("Informe a senha do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a email do assinante.");

            if (string.IsNullOrWhiteSpace(dto.Foto))
                _erros.Add("Informe a foto do assinante.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override async Task<Assinante> DefinirEntidadeAlteracao(Assinante entidade, AssinanteDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Assinante informado não encontrada.");
            if (dto.Login != entidade.Login)
               _erros.Add("Não é possível alterar o login do assinante.");
            if (dto.Senha != entidade.Senha)
               _erros.Add("Para alterar a senha do assinante utilize o metodo alterar senha.");
            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));
            entidade.AlterarDadosDoUsuario(dto.Nome, dto.Email, dto.Foto);
            return entidade;
        }
        protected override void ValidarDelecao(Assinante entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Assinante informada não encontrada.");
        }

        public override async Task<AssinanteRetornoDto> ObterPorIdAsync(Guid id)
        {
            return _mapper.Map<AssinanteRetornoDto>(await _repository.ObterAssinantePorId(id));
        }

        public override async Task<IReadOnlyList<AssinanteRetornoDto>> ObterTodosAsync()
        {
            return _mapper.Map<IReadOnlyList<AssinanteRetornoDto>>(await _repository.ObterAssinantes());
        }

        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto , Guid id)
        {
            await _usuarioService.AlterarSenha(alterarSenhaDto, id);
        }

        public async Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            await _usuarioService.RecuperarSenha(usuarioSenhaDto);
        }
        public string Autenticar(LoginDto loginDto)
        {
            return _usuarioService.Autenticar(loginDto);
        }

        public async Task AssinarAsync(AssinarDto assinarDto)
        {

            var assinanteId = ObterUserId();
            if (Guid.Empty == assinanteId || assinanteId == null)
                throw new Exception("Não foi possível realizar a assinatura");

            var assinatura = await _assinaturaRepository.ObterPorIdAsync(assinarDto.AssinaturaId);
            if (assinatura == null)
                throw new Exception("Não foi possível realizar a assinatura. Assinatura informada não encontrada");

            var usuario = await _repository.ObterAssinantePorId(assinanteId.Value);
            if (usuario == null)
                throw new Exception("Não foi possível realizar a assinatura. Usuário não encontrado");

            usuario.DefinirAssinatura(assinatura);
            await _repository.AlterarAsync(usuario);
        }

        private Guid? ObterUserId()
        {
            var claims = _accessor.HttpContext.User.Identity as ClaimsIdentity;

            var assinanteId = _accessor.HttpContext.User.FindFirst("Id");
            return Guid.Parse(assinanteId?.Value);
        }
    }

}

using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Autor;
using AutoMapper;
using Dominio.Entidades;
using Dominio.ObjetosDeValor;

namespace Aplicacao
{
    public class AutorService : ServiceBase<AutorRetornoDto, AutorDto, Autor, IRepositoryBase<Autor>>, IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IRepositoryBase<RedeSocial> _redeSocialRepository;
        private readonly IUsuarioService<Autor> _usuarioService;

        public AutorService(
            IRepositoryBase<Autor> repository,
            IMapper mapper,
            IRepositoryBase<RedeSocial> redeSocialRepository,
            IAutorRepository autorRepository,
            IUsuarioService<Autor> usuarioService) : base(repository, mapper)
        {
            _redeSocialRepository = redeSocialRepository;
            _autorRepository = autorRepository;
            _usuarioService = usuarioService;
        }

        protected override async Task<Autor> DefinirEntidadeInclusaoAsync(AutorDto dto)
        {
            var usuario = Repository.ObterIQueryable().FirstOrDefault(x => x.Login == dto.Login);

            if (usuario != null)
                throw new Exception("Login informado não disponível.");

            var autor = new Autor(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto, dto.Descricao);
            return autor;
        }

        protected override void ValidarValores(AutorDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados do autor.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                _erros.Add("Informe o nome do autor.");

            if (string.IsNullOrWhiteSpace(dto.Login))
                _erros.Add("Informe o login do autor.");
            
            if (string.IsNullOrWhiteSpace(dto.Senha))
                _erros.Add("Informe a senha do autor.");
            
            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a email do autor.");

            if (string.IsNullOrWhiteSpace(dto.Foto))
                _erros.Add("Informe a foto do autor.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                _erros.Add("Informe a descricao do autor.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override async Task<Autor> DefinirEntidadeAlteracao(Autor entidade, AutorDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Autor informado não encontrada.");
            entidade.AlterarDadosDoUsuario(dto.Nome, dto.Email, dto.Foto);
            entidade.DefinirDescricao(dto.Descricao);
            return entidade;
        }
        protected override void ValidarDelecao(Autor entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Autor informada não encontrada.");
        }


        public override async Task<AutorRetornoDto> ObterPorIdAsync(Guid id)
        {            
            return _mapper.Map<AutorRetornoDto>(await _autorRepository.ObterAutorPorId(id));            
        }
        public override async Task<IReadOnlyList<AutorRetornoDto>> ObterTodosAsync()
        {            
            return _mapper.Map<IReadOnlyList<AutorRetornoDto>>(await _autorRepository.ObterAutores());            
        }
        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            await _usuarioService.AlterarSenha(alterarSenhaDto);
        }

        public async Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            await _usuarioService.RecuperarSenha(usuarioSenhaDto);
        }
        public string Autenticar(LoginDto loginDto)
        {
            return _usuarioService.Autenticar(loginDto);
        }

        public async Task AdicionarRedeSocial(Guid idAutor, RedeSocialDto redeSocialDto)
        {
            Autor autor = await _autorRepository.ObterAutorPorId(idAutor);
            var redeSocial = new RedeSocial(redeSocialDto.Nome, redeSocialDto.Link);
            autor.AdicionarRedeSocial(redeSocial);
            await _redeSocialRepository.AdicionarAsync(redeSocial);
            await _autorRepository.AlterarAsync(autor);
        }

        public async Task RemoverRedeSocial(Guid idAutor, Guid idRedeSocial)
        {
            Autor autor = await _autorRepository.ObterAutorPorId(idAutor);
            RedeSocial redeSocial = await _redeSocialRepository.ObterPorIdAsync(idRedeSocial);
            autor.RemoverRedeSocial(redeSocial);
            await _redeSocialRepository.DeletarAsync(redeSocial);
            await _autorRepository.AlterarAsync(autor);
        }
    }

}

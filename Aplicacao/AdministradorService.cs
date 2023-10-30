using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Administrador;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao
{
    public class AdministradorService : ServiceBase<AdministradorRetornoDto, AdministradorDto , Administrador, IRepositoryBase<Administrador>>, IAdministradorService
    {
        private readonly IUsuarioService<Administrador> _usuarioService;
        public AdministradorService(
            IRepositoryBase<Administrador> repository, 
            IMapper mapper, 
            IUsuarioService<Administrador> usuarioService) : base(repository, mapper)
        {
            _usuarioService = usuarioService;
        }

        protected override async Task<Administrador> DefinirEntidadeInclusaoAsync(AdministradorDto dto)
        {
            var usuario = Repository.ObterIQueryable().FirstOrDefault(x => x.Login == dto.Login);

            if (usuario != null)            
                throw new Exception("Login informado não disponível.");
            
            return new Administrador(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto);
        }

        protected override void ValidarValores(AdministradorDto dto)
        {            
            if (dto == null)
                _erros.Add("Informe os dados do administrador");

            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a descrição do administrador.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override async Task<Administrador> DefinirEntidadeAlteracao(Administrador entidade, AdministradorDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Administrador informada não encontrado.");
            entidade.AlterarDadosDoUsuario(dto.Nome, dto.Email, dto.Foto);

            return entidade;
        }
        protected override void ValidarDelecao(Administrador entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Administrador informada não encontrado.");
        }

        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto, Guid id)
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
    }

}

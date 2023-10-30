using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aplicacao
{
    public class UsuarioService<TEntity> : IUsuarioService<TEntity>
        where TEntity : Usuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto, Guid id)
        {
            Usuario usuario = await _usuarioRepository.ObterPorIdAsync(id);
            ValidarAlterarSenha(alterarSenhaDto, usuario);
            if (usuario != null)
            {
                usuario.AlterarSenha(alterarSenhaDto.NovaSenha);
                await _usuarioRepository.AlterarAsync(usuario);
                return;
            }
            throw new Exception("Login informado não encontrado");
        }

        private void ValidarAlterarSenha(AlterarSenhaDto alterarSenhaDto, Usuario usuario)
        {
            if (alterarSenhaDto.Login != usuario.Login)
                throw new Exception("O usuario atual não corresponde ao do login informado");
            if (alterarSenhaDto.SenhaAtual != usuario.Senha)
                throw new Exception("Senha atual não confere");
            if (alterarSenhaDto == null || string.IsNullOrWhiteSpace(alterarSenhaDto.NovaSenha))
                throw new Exception("Informe a senha");
            if ((!string.IsNullOrWhiteSpace(alterarSenhaDto.NovaSenha) && !string.IsNullOrWhiteSpace(alterarSenhaDto.ConfirmacaoDeNovaSenha))
                && (alterarSenhaDto.NovaSenha != alterarSenhaDto.ConfirmacaoDeNovaSenha))
                throw new Exception("Senha e Confirmação de senha não conferem");
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretApi"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim(ClaimTypes.Role, usuario.Tipo.ToString()),
                    new Claim("Id", usuario.Id.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            if (usuarioSenhaDto == null || string.IsNullOrWhiteSpace(usuarioSenhaDto.Login))
                throw new Exception("Informe o login");
            var usuario = _usuarioRepository.ObterIQueryable().OfType<TEntity>().Where(x => x.Login == usuarioSenhaDto.Login).FirstOrDefault();
            if (usuario != null)
            {
                var novaSenha = usuario.GerarNovaSenha();
                var usuarioSenha = new RecuperarSenhaDto
                {
                    Login = usuario.Login,
                    Senha = novaSenha,
                };

                // TODO - Enviar a senha por email

                usuario.AlterarSenha(novaSenha);
                await _usuarioRepository.AlterarAsync(usuario);
                return;
            }

            throw new Exception("Login informado não encontrado");
        }

        public Usuario ObterPorLoginESenha(LoginDto logindto)
        {
            var usuario = _usuarioRepository.ObterIQueryable().OfType<TEntity>().Where(x => x.Login == logindto.Login && x.Senha == logindto.Senha).FirstOrDefault();
            return usuario;
        }
        public string Autenticar(LoginDto loginDto)
        {
            var usuario = ObterPorLoginESenha(loginDto);
            if (usuario != null)
            {
                var token = GerarToken(usuario);
                return token;
            }
            return string.Empty;
        }
    }
}

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

        public async Task AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            if (alterarSenhaDto == null || string.IsNullOrWhiteSpace(alterarSenhaDto.Senha))
                throw new Exception("Informe a senha");

            if ((!string.IsNullOrWhiteSpace(alterarSenhaDto.Senha) && !string.IsNullOrWhiteSpace(alterarSenhaDto.ConfirmacaoDeSenha))
                && (alterarSenhaDto.Senha != alterarSenhaDto.ConfirmacaoDeSenha))
                throw new Exception("Senha e Confirmação de senha não conferem");

            var usuario = _usuarioRepository.ObterIQueryable().OfType<TEntity>().Where(x => x.Login == alterarSenhaDto.Login).FirstOrDefault();
            if (usuario != null)
            {
                usuario.AlterarSenha(alterarSenhaDto.Senha);
                await _usuarioRepository.AlterarAsync(usuario);
                return;
            }
            throw new Exception("Login informado não encontrado");
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

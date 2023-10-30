using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Autor;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "AUTOR, ADMINISTRADOR")]
    public class AutorController : BaseController<Autor, AutorRetornoDto, AutorDto, IAutorService>
    {
        private readonly IAutorService appService;

        public AutorController(IAutorService appService) : base(appService)
        {
            this.appService = appService;
        }

        [HttpPut("AlterarSenha")]
        [Authorize(Roles = "AUTOR, ADMINISTRADOR")]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                await appService.AlterarSenha(alterarSenhaDto);
                return Ok("Senha alterado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("RecuperarSenha")]
        [Authorize(Roles = "AUTOR, ADMINISTRADOR")]
        public async Task<IActionResult> RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            try
            {
                await appService.RecuperarSenha(usuarioSenhaDto);
                return Ok("Senha recuperada com sucesso. Verifique o email de cadastro");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto usuario)
        {
            try
            {
                var token = appService.Autenticar(usuario);
                if (string.IsNullOrWhiteSpace(token))
                    return StatusCode(StatusCodes.Status400BadRequest, "Dados Informados inválidos");
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AdicionarRedeSocial")]
        [Authorize(Roles = "AUTOR")]
        public async Task<IActionResult> AdicionarRedeSocial(RedeSocialDto redeSocialDto)
        {
            try
            {
                var idAutor = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                await appService.AdicionarRedeSocial(idAutor, redeSocialDto);
                return Ok("Rede social adicionada com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("RemoverRedeSocial/{id}")]
        [Authorize(Roles = "AUTOR")]
        public async Task<IActionResult> RemoverRedeSocial(Guid id)
        {
            try
            {
                var idAutor = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                await appService.RemoverRedeSocial(idAutor, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Administrador;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class AdministradorController : BaseController<Administrador, AdministradorRetornoDto, AdministradorDto, IAdministradorService>
    {
        private readonly IAdministradorService appService;

        public AdministradorController(IAdministradorService appService) : base(appService)
        {
            this.appService = appService;
        }

        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                var id = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                await appService.AlterarSenha(alterarSenhaDto, id);
                return Ok("Senha alterado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("RecuperarSenha")]
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
    }
}

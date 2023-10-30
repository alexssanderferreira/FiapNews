using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Assinante;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR, ASSINANTE")]
    public class AssinanteController : BaseController<Assinante, AssinanteRetornoDto, AssinanteDto, IAssinanteService>
    {
        private readonly IAssinanteService appService;

        public AssinanteController(IAssinanteService appService) : base(appService)
        {
            this.appService = appService;
        }

        [AllowAnonymous]
        public override Task<IActionResult> AdicionarAsync(AssinanteDto dto)
        {
            return base.AdicionarAsync(dto);
        }

        [Authorize(Roles = "ASSINANTE, ADMINISTRADOR")]
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                var id = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                await appService.AlterarSenha(alterarSenhaDto, id);
                return Ok("Senha alterado com sucesso");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "ASSINANTE, ADMINISTRADOR")]
        [HttpPut("RecuperarSenha")]
        public async Task<IActionResult> RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto)
        {
            try
            {
                await appService.RecuperarSenha(usuarioSenhaDto);
                return Ok("Senha recuperada com sucesso. Verifique o email de cadastro");
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "ASSINANTE")]
        [HttpPost("Assinar")]
        public async Task<IActionResult> Assinar(AssinarDto assinarDto)
        {
            try
            {
                await appService.AssinarAsync(assinarDto);
                return Ok("Assinatura realizada com sucesso");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeletarAsync(Guid id)
        {
            try
            {
                await Service.DeletarAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("Obter-Todos")]
        public override async Task<IActionResult> ObterTodosAsync()
        {
            return Ok(await Service.ObterTodosAsync());
        }

        [AllowAnonymous]
        [HttpGet("Obter-Por-Id")]
        public override async Task<IActionResult> ObterPorIdAsync(Guid id)
        {
            return Ok(await Service.ObterPorIdAsync(id));
        }
    }
}

using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Tag;
using Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Authorize(Roles = "ADMINISTRADOR")]
public class TagController : BaseController<Tag, TagRetornoDto, TagDto, ITagService>
{
    private readonly ITagService appService;
    public TagController(ITagService appService) : base(appService)
    {
        this.appService = appService;
    }

    [HttpGet("Obter-Por-Texto/{texto}")]
    [AllowAnonymous]
    public IActionResult ObterPorTexto(string texto)
    {
        try
        {
            var tag = Service.ObterPorTexto(texto);
            if (tag == null) NoContent();
            return Ok(tag);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
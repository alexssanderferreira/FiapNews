using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Noticia;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Authorize(Roles = "AUTOR, ADMINISTRADOR")]
public class NoticiaController : BaseController<Noticia, NoticiaRetornoDto, NoticiaDto, INoticiaService>
{

    public NoticiaController(INoticiaService appService) : base(appService) { }

    [HttpGet("Obter-Por-Categoria/{id}")]
    public IActionResult ObterNoticiaPorCategoria(Guid id)
    {
        try
        {
            return Ok(Service.ObterNoticiaCategoria(id));
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [AllowAnonymous]
    public override Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        return base.ObterPorIdAsync(id);
    }

    [AllowAnonymous]
    public override Task<IActionResult> ObterTodosAsync()
    {
        return base.ObterTodosAsync();
    }

}
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BaseController<TEntity, TDto, TDtoMod, TService> : ControllerBase
    where TEntity : Base
    where TDto : BaseDto
    where TDtoMod : class
    where TService : IServiceBase<TDto, TDtoMod>
{
    protected TService Service;

    public BaseController(TService appService)
    {
        Service = appService;
    }

    [HttpPost]
    public virtual async Task<IActionResult> AdicionarAsync(TDtoMod dto)
    {
        try
        {
            await Service.AdicionarAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> AlterarAsync(Guid id, TDtoMod dto)
    {
        try
        {
            await Service.AlterarAsync(dto, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> ObterTodosAsync()
    {
        return Ok(await Service.ObterTodosAsync());
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> ObterPorIdAsync(Guid id)
    {
        return Ok(await Service.ObterPorIdAsync(id));
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeletarAsync(Guid id)
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
}

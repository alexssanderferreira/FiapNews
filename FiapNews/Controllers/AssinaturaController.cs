using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Assinatura;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ADMINISTRADOR")]
public class AssinaturaController : BaseController<Assinatura, AssinaturaRetornoDto, AssinaturaDto, IAssinaturaService>
{
    public AssinaturaController(IAssinaturaService appService) : base(appService)
    {
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

using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Categoria;
using Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class CategoriaController : BaseController<Categoria, CategoriaRetornoDto, CategoriaDto, ICategoriaService>
    {
        private readonly ICategoriaService appService;

        public CategoriaController(ICategoriaService appService) : base(appService)
        {
            this.appService = appService;
        }

        [AllowAnonymous]
        public override Task<IActionResult> ObterTodosAsync()
        {
            return base.ObterTodosAsync();
        }

        [AllowAnonymous]
        public override Task<IActionResult> ObterPorIdAsync(Guid id)
        {
            return base.ObterPorIdAsync(id);
        }
    }
}

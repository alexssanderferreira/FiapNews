using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.RedeSocial;
using Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapNews.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class RedeSocialController : BaseController<RedeSocial, RedeSocialRetornoDto, RedeSocialDto, IRedeSocialService>
    {
        private readonly IRedeSocialService appService;

        public RedeSocialController(IRedeSocialService appService) : base(appService)
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

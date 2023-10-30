using Aplicacao.DTOs;
using Aplicacao.DTOs.Assinante;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IAssinanteService : IServiceBase<AssinanteRetornoDto , AssinanteDto>, IUsuarioService<Assinante>
    {
        Task AssinarAsync(AssinarDto assinarDto);

    }

}

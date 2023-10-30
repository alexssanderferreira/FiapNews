using Aplicacao.DTOs.Administrador;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IAdministradorService : IServiceBase<AdministradorRetornoDto, AdministradorDto>, IUsuarioService<Administrador>
    {        
    }
}

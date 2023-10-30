using Aplicacao.DTOs;
using Aplicacao.DTOs.Autor;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IAutorService : IServiceBase<AutorRetornoDto, AutorDto>, IUsuarioService<Autor>
    {
        Task AdicionarRedeSocial(Guid idAutor,RedeSocialDto redeSocialDto);
        Task RemoverRedeSocial(Guid idAutor, Guid redeSocialId);
    }

}

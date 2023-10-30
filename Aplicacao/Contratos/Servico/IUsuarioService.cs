using Aplicacao.DTOs;
using Dominio.Entidades;

namespace Aplicacao.Contratos.Servico
{
    public interface IUsuarioService<TEntity> where TEntity : Usuario
    {
        Task AlterarSenha(AlterarSenhaDto alterarSenhaDto , Guid id);
        Task RecuperarSenha(UsuarioSenhaDto usuarioSenhaDto);
        string Autenticar(LoginDto loginDto);
    }
}

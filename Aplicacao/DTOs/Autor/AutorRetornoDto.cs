using Aplicacao.DTOs.RedeSocial;
using Aplicacao.DTOs.Usuario;

namespace Aplicacao.DTOs.Autor;

public class AutorRetornoDto : UsuarioRetornoDto
{
    public string Descricao { get; set; }
    public List<RedeSocialRetornoDto> RedesSociais { get; set; } = new List<RedeSocialRetornoDto>();
}

using Aplicacao.DTOs.Assinatura;
using Aplicacao.DTOs.Usuario;

namespace Aplicacao.DTOs.Assinante;

public class AssinanteRetornoDto : UsuarioRetornoDto
{
    public AssinaturaRetornoDto Assinatura { get; set; }
}

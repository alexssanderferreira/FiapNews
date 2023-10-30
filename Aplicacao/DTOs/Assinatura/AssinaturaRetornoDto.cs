using Dominio.Enum;

namespace Aplicacao.DTOs.Assinatura;

public class AssinaturaRetornoDto : BaseDto
{
    public TipoAssinatura TipoAssinatura { get; set; }
    public double Preco { get; set; }
    public int TipoPlano { get; set; }
}

using Dominio.Enum;

namespace Aplicacao.DTOs;
public class AssinaturaDto
{
    public TipoAssinatura TipoAssinatura { get;  set; }
    public double Preco { get; set; }
    public int TipoPlano { get; set; }
}
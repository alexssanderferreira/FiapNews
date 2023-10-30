using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs.Categoria;

public class CategoriaRetornoDto : BaseDto
{
    [Required(ErrorMessage = "Descrição é obrigatória")]
    public string Descricao { get; set; }
}

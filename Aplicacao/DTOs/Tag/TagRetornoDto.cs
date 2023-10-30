using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs.Tag;

public class TagRetornoDto : BaseDto
{
    [Required(ErrorMessage = "Texto é requerido")]
    public string Texto { get; set; }
}

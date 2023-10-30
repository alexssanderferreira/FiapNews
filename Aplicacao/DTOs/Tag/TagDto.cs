using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs;

public class TagDto
{
    [Required(ErrorMessage = "Texto é requerido")]
    public string Texto { get; set; }
}

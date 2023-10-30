using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs
{
    public class RedeSocialDto
    {
        [Required(ErrorMessage = "Nome é obrigatória")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Link é obrigatória")]
        public string Link { get; set; }
    }
}

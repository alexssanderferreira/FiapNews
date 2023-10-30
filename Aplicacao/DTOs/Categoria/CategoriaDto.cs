using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs
{
    public class CategoriaDto
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; }
    }
}

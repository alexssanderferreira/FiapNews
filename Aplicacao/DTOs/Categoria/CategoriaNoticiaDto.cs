using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.DTOs.Categoria;

public class CategoriaNoticiaDto : BaseDto
{
    [Required(ErrorMessage = "Descrição é obrigatória")]
    public string Descricao { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.DTOs.Usuario;

public class UsuarioRetornoDto : BaseDto
{
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Email { get; set; }
    public string Foto { get; set; }
}

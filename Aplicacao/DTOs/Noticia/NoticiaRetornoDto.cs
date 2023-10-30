using Aplicacao.DTOs.Autor;
using Aplicacao.DTOs.Categoria;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs.Noticia;

public class NoticiaRetornoDto : BaseDto
{
    public string Titulo { get; set; }
    public string SubTitulo { get; set; }
    public string Conteudo { get; set; }
    public string Lead { get; set; }
    public DateTime DataCriacao { get; set; }
    public ICollection<AutorRetornoDto> Autores { get; set; }
    public ICollection<CategoriaRetornoDto> Categorias { get; set; }
    public string Regiao { get; set; }
    public string LinkDeCompartilhamento { get; set; }
    public ICollection<string> Imagens { get; set; }
}

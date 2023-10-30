using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Noticia;
using AutoMapper;
using Dominio.Entidades;
using Dominio.ObjetosDeValor;
using Microsoft.IdentityModel.Tokens;

namespace Aplicacao;
public class NoticiaService : ServiceBase<NoticiaRetornoDto, NoticiaDto, Noticia, INoticiaRepository>, INoticiaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IAutorRepository _autorRepository;
    public NoticiaService(INoticiaRepository repository, IMapper mapper, ICategoriaRepository categoriaRepository, IAutorRepository autorRepository) : base(repository, mapper)
    {
        _categoriaRepository = categoriaRepository;
        _autorRepository = autorRepository;
    }

    protected override async Task<Noticia> DefinirEntidadeAlteracao(Noticia entidade, NoticiaDto dto)
    {
       if(entidade == null) throw new ArgumentNullException(nameof(entidade), "Noticia informada não encontrada.");
        
        entidade.DefinirTitulo(dto.Titulo);
        entidade.DefinirSubtitulo(dto.SubTitulo);
        entidade.DefinirConteudo(dto.Conteudo);
        entidade.DefinirLead(dto.Lead);
        
        if (dto.Categorias is not null)
        {
            entidade.LimparCategoria();
            foreach (var categoriadto in dto.Categorias)
            {
                Categoria categoria = await _categoriaRepository.ObterPorIdAsync(categoriadto.Id);
                if (categoria == null) _erros.Add($"Categoria {categoriadto.Id} informada não encontrada.");
                entidade.AdicionarCategoria(categoria);
            }
        }
        
        if(dto.Autores is not null)
        {
            entidade.LimparAutores();
            foreach (var autordto in dto.Autores)
            {
                Autor autor = await _autorRepository.ObterPorIdAsync(autordto.Id);
                if (autor == null) _erros.Add("Autor informada não encontrada.");
                entidade.AdicionarAutor(autor);
            }
        }
        
        entidade.DefinirRegiao(dto.Regiao);
        dto.Imagens.ToList().ForEach(i => entidade.AdicionarImagem(i)); 

        return entidade;
    }

    protected override async Task<Noticia> DefinirEntidadeInclusaoAsync(NoticiaDto dto)
    {
        ICollection<Autor> autores = new List<Autor>();
        ICollection<Categoria> categorias = new List<Categoria>();

        foreach (var categoriaDto in dto.Categorias)
        {
            Categoria categoria = await _categoriaRepository.ObterPorIdAsync(categoriaDto.Id);
            if (categoria == null) _erros.Add($"Categoria {categoriaDto.Id} informada não encontrada.");
            categorias.Add(categoria);
        }
        foreach (var autorDto in dto.Autores)
        {
            Autor autor = await _autorRepository.ObterPorIdAsync(autorDto.Id);
            if (autor == null) _erros.Add($"Autor {autorDto.Id} informada não encontrada.");
            autores.Add(autor);
        }

        if(categorias.IsNullOrEmpty())
            _erros.Add("Informe uma categoria");
        if(autores.IsNullOrEmpty())
            _erros.Add("Informe um autor");
            
        return new Noticia(dto.Titulo, dto.SubTitulo, dto.Conteudo, dto.Lead, categorias, autores, dto.Regiao, false, dto.Imagens);
    }

    protected override void ValidarDelecao(Noticia entidade)
    {
        if (entidade == null)
            throw new ArgumentNullException(nameof(entidade), "Noticia informada não encontrada.");
    }

    protected override void ValidarValores(NoticiaDto dto)
    {
        if (dto == null) _erros.Add("Informe os dados da Noticia!");
        if (string.IsNullOrWhiteSpace(dto.Conteudo)) _erros.Add("informe o conteudo da noticia");
        if (string.IsNullOrWhiteSpace(dto.Titulo)) _erros.Add("informe o titulo da noticia");
        if (string.IsNullOrWhiteSpace(dto.SubTitulo)) _erros.Add("informe o sub titulo da noticia");
        if (string.IsNullOrWhiteSpace(dto.Lead)) _erros.Add("informe o lead da noticia");
        if (!dto.Categorias.Any()) _erros.Add("Informe uma categoria");
        if (!dto.Autores.Any()) _erros.Add("Informe uma autor");
        if (_erros.Any()) throw new Exception(string.Join("\n", _erros));
    }

    public IList<NoticiaRetornoDto> ObterNoticiaCategoria(Guid idCategoria)
    {
        var noticias = Repository.ObterNoticiaCategoria(idCategoria);
        IList<NoticiaRetornoDto> dtos = new List<NoticiaRetornoDto>();

        foreach (var item in noticias)
        {
            var noticia = _mapper.Map<NoticiaRetornoDto>(item);
            dtos.Add(noticia);
        }
        return dtos;
    }
}

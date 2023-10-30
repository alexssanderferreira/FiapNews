using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Aplicacao.DTOs.Tag;
using AutoMapper;
using Dominio.ObjetosDeValor;

namespace Aplicacao;

public class TagService : ServiceBase<TagRetornoDto, TagDto, Tag, ITagRepository>, ITagService
{
    public TagService(ITagRepository repository, IMapper mapper) : base(repository, mapper)
    {
        
    }

    protected override void ValidarValores(TagDto tag)
    {
        if (tag == null) _erros.Add("Informe os dados da Tag");
        if (string.IsNullOrWhiteSpace(tag.Texto)) _erros.Add("Informe o texto da Tag");
        if (_erros.Any()) throw new Exception(string.Join("\n", _erros));
    }

    protected override async Task<Tag> DefinirEntidadeAlteracao(Tag entidade, TagDto dto)
    {
        if(entidade == null)
            throw new ArgumentNullException(nameof(entidade), "Tag informada não encontrada.");

        var tag = ObterPorTexto(dto.Texto);
            if (tag != null) throw new ArgumentNullException(nameof(dto), "Já existe uma tag com esse nome");

        entidade.AlteraTexto(dto.Texto);
        return entidade;
    }

    protected override async Task<Tag> DefinirEntidadeInclusaoAsync(TagDto dto)
    {

        var tag = ObterPorTexto(dto.Texto);
            if (tag != null) throw new ArgumentNullException($"Já existe uma tag com esse nome {dto.Texto}");

        return new Tag(dto.Texto);
    }

    protected override void ValidarDelecao(Tag entidade)
    {
        if (entidade == null)
            throw new ArgumentNullException(nameof(entidade), "Tag informada não encontrada.");
    }

    public TagRetornoDto ObterPorTexto(string texto)
    {
        var tag = Repository.ObterPorTexto(texto);
        return _mapper.Map<TagRetornoDto>(tag);
    }
}

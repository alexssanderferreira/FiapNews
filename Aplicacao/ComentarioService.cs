using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs.Comentario;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Enum;

namespace Aplicacao;

public class ComentarioService : IComentarioService
{
    private readonly IMapper _mapper;
    private readonly IComentarioRepository _comentarioRepository;
    private readonly IAdministradorRepository _administradorRepository;
    private readonly INoticiaRepository _noticiaRepository;
    private readonly IAssinanteRepository _assinanteRepository;
    private readonly IAutorRepository _autorRepository;
    protected List<string> _erros;

    public ComentarioService(IComentarioRepository comentarioRepository, IMapper mapper, IAdministradorRepository administradorRepository, INoticiaRepository noticiaRepository, IAssinanteRepository assinanteRepository, IAutorRepository autorRepository)
    {
        _comentarioRepository = comentarioRepository;
        _administradorRepository = administradorRepository;
        _noticiaRepository = noticiaRepository;
        _assinanteRepository = assinanteRepository;
        _autorRepository =  autorRepository;
        _mapper = mapper;
        _erros = new List<string>();
    }

    //Aprovar ou Reprovar comentario
    public async Task Aprovar(Guid idComentario, Guid idAdministrador)
    {
        Comentario comentario = await _comentarioRepository.ObterPorIdAsync(idComentario);

        if (comentario is null)
            throw new Exception("Comentario não encontrado.");

        Administrador administrador = await _administradorRepository.ObterPorIdAsync(idAdministrador);

        comentario.AprovarComentario(administrador);
        await _comentarioRepository.AlterarAsync(comentario);
    }
    public async Task Reprovar(Guid idComentario, Guid idAdministrador, string motivo)
    {
        Comentario comentario = await _comentarioRepository.ObterPorIdAsync(idComentario);

        if (comentario is null)
            throw new Exception("Comentario não encontrado.");

        Administrador administrador = await _administradorRepository.ObterPorIdAsync(idAdministrador);

        comentario.ReprovarComentario(administrador, motivo);
        await _comentarioRepository.AlterarAsync(comentario);
    }

    //Todos
    public async Task<IEnumerable<ComentarioRetornoDto>> GetAprovados()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Aprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetReprovados()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Reprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetPendentes()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Pendente);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }

    // PorNoticia
    public async Task<IEnumerable<ComentarioRetornoDto>> GetAprovadosPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia , EstadoValidacaoComentario.Aprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetReprovadosPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia, EstadoValidacaoComentario.Reprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetPendentesPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia, EstadoValidacaoComentario.Pendente);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }

    //ValidarComentario
    private void ValidarValoresDto(ComentarioDto comentarioDto)
    {
        if (comentarioDto == null)
            _erros.Add("Informe os dados do comentario.");

        if (string.IsNullOrWhiteSpace(comentarioDto.Texto))
            _erros.Add("Informe a texto do comentario.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));

        return;
    }
    private void ValidarValores(Comentario entidade)
    {
        if (entidade == null)
            _erros.Add("Informe os dados do comentario.");

        if (string.IsNullOrWhiteSpace(entidade.Texto))
            _erros.Add("Informe a texto do comentario.");

        if (entidade.Noticia is null)
            _erros.Add("Noticia informada não encontrada.");

        if (entidade.Usuario is null)
            _erros.Add("Assinante informado não encontrado.");

        if (!entidade.Usuario.PodeComentar)
            _erros.Add(entidade.Usuario.Nome + " não pode comentar.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));
    }   
    private void ValidarDelecao(Comentario entidade)
    {
        if (entidade == null)
            _erros.Add("Comentario informada não encontrada.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));
    }

    //MetodosBasicos
    public async Task AdicionarAsync(ComentarioDto dto,Guid usuarioId, string role)
    {
        ValidarValoresDto(dto);
        Noticia noticia = await _noticiaRepository.ObterPorIdAsync(dto.NoticiaId);
        Usuario usuario = await RetornarUsuarioAsync(usuarioId, role);
        Comentario comentario = new(dto.Texto, usuario, noticia);
        ValidarValores(comentario);

        await _comentarioRepository.AdicionarAsync(comentario);
    }
    public async Task<IReadOnlyList<ComentarioRetornoDto>> ObterTodosAsync()
    {
        var comentarios = await _comentarioRepository.ObterTodosAsync();
        return _mapper.Map<IReadOnlyList<ComentarioRetornoDto>>(comentarios);
    }
    public async Task<ComentarioRetornoDto> ObterPorIdAsync(Guid id)
    {
        var comentario = await _comentarioRepository.ObterPorIdAsync(id);
        return _mapper.Map<ComentarioRetornoDto>(comentario);
    }
    public async Task DeletarAsync(Guid id)
    {
        var comentario = await _comentarioRepository.ObterPorIdAsync(id);
        ValidarDelecao(comentario);
        await _comentarioRepository.DeletarAsync(comentario);
    }

    private async Task<Usuario> RetornarUsuarioAsync(Guid idUsuario,string role)
    {
        Usuario usuario = null;
        switch (role)
        {
            case "AUTOR":
                usuario = await _autorRepository.ObterAutorPorId(idUsuario);
                break;
            case "ASSINANTE":
                usuario = await _assinanteRepository.ObterAssinantePorId(idUsuario);
                break;
            case "ADMINISTRADOR":
                usuario = await _administradorRepository.ObterPorIdAsync(idUsuario);
                break;
        }

        if (usuario is null)
            _erros.Add("Usuario não encontrado.");

        return usuario;
    }
}

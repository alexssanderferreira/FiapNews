using Dominio.Enum;

namespace Dominio.Entidades;

public class Comentario : Base
{
    private const int LIMITE_COMENTARIO = 1000;
    private const int LIMITE_REJEICAO = 500;
    public string Texto { get; private set; }
    public Usuario Usuario { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Noticia Noticia { get; private set; }
    public EstadoValidacaoComentario EstadoValidacao { get; private set; }
    public DateTime? DataValidacao { get; private set; }
    public Administrador ModeradorResponsavel { get; private set; }
    public string MotivoRejeicao { get; private set; }

    private IList<string> _erros = new List<string>();

    protected Comentario()
    {

    }

    public Comentario(string texto, Usuario usuario, Noticia noticia) : base()
    {
        DataCriacao = DateTime.UtcNow;
        EstadoValidacao = EstadoValidacaoComentario.Pendente;

        ValidarCriacao(texto, noticia, usuario);
        DefinirUsuario(usuario);
        DefinirNoticia(noticia);
        DefinirTexto(texto);
    }

    private void DefinirUsuario(Usuario usuario)
    {
        Usuario = usuario;
    }

    private void DefinirNoticia(Noticia noticia)
    {
        Noticia = noticia;
    }

    private void DefinirTexto(string texto)
    {
        Texto = texto;
    }

    public void AprovarComentario(Administrador moderador)
    {
        ValidarModeracao(moderador, EstadoValidacaoComentario.Aprovado, string.Empty);
        DefinirStatusComentario(moderador, EstadoValidacaoComentario.Aprovado, string.Empty);
    }

    public void ReprovarComentario(Administrador moderador, string motivo)
    {
        ValidarModeracao(moderador, EstadoValidacaoComentario.Reprovado, motivo);
        DefinirStatusComentario(moderador, EstadoValidacaoComentario.Reprovado, motivo);
    }

    private void DefinirStatusComentario(Administrador moderador, EstadoValidacaoComentario estado, string motivo)
    {
        MotivoRejeicao = motivo;
        EstadoValidacao = estado;
        DataValidacao = DateTime.UtcNow;
        ModeradorResponsavel = moderador;
    }

    public void ValidarModeracao(Administrador moderador, EstadoValidacaoComentario estado, string motivo)
    {
        if (moderador == null)
            _erros.Add("Moderador é obrigatório!");

        if (EstadoValidacao != EstadoValidacaoComentario.Pendente)
            _erros.Add("Comentário já foi validado!");

        if (estado == EstadoValidacaoComentario.Pendente)
            _erros.Add("Estado de validação é obrigatório, e não pode ser definido como pendente!");

        if (estado == EstadoValidacaoComentario.Reprovado && string.IsNullOrWhiteSpace(motivo))
            _erros.Add("Motivo de rejeição é obrigatório!");

        if (motivo.Length > LIMITE_REJEICAO)
            _erros.Add($"Motivo de rejeição deve ter no máximo {LIMITE_REJEICAO} caracteres!");

        if (_erros.Any())
            throw new ArgumentException(string.Join("\n", _erros));
    }

    public void ValidarCriacao(string texto, Noticia noticia, Usuario usuario)
    {
        if (usuario == null)
            _erros.Add("Usuário é obrigatório!");

        if (noticia == null)
            _erros.Add("Notícia é obrigatória!");

        if (string.IsNullOrWhiteSpace(texto))
            _erros.Add("Texto é obrigatório!");

        if (texto.Length > LIMITE_COMENTARIO)
            _erros.Add($"Texto deve ter no máximo {LIMITE_COMENTARIO} caracteres!");

        if (texto.Contains("http://") || texto.Contains("https://") || texto.Contains("www."))
            _erros.Add("Texto não pode conter links!");

        if (_erros.Any())
            throw new ArgumentException(string.Join("\n", _erros));
    }
}

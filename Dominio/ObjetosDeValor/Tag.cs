using Dominio.Entidades;

namespace Dominio.ObjetosDeValor;
public class Tag : Base, IEquatable<Tag>
{
    public string Texto { get; private set; }
    public IReadOnlyCollection<Noticia> Noticias { get; }

    protected Tag(){}

    public Tag(string texto)
    {
        AlteraTexto(texto);
    }

    public bool Equals(Tag other)
    {
        if (ReferenceEquals(null, other)) 
            return false;

        return other.Texto == Texto;
    }

    public void AlteraTexto(string texto)
    {
        if(string.IsNullOrWhiteSpace(texto)) throw new ArgumentNullException("Texto da tag não pode ser vazio ");
        Texto = texto;
    }

}

using Dominio.Entidades;

namespace Dominio.ObjetosDeValor
{
    public class RedeSocial : Base
    {
        public string Nome { get; private set; } = string.Empty;
        public string Link { get; private set; } = string.Empty;
        public IReadOnlyCollection<Autor> Autores { get; set; }
        protected RedeSocial()
        {

        }

        public RedeSocial(string descricao, string link) : base()
        {
            DefinirNome(descricao);
            DefinirLink(link);
        }

        public void DefinirNome(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        private void ValidarNome(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) 
                throw new ArgumentNullException(nameof(descricao), "O Nome da rede social não pode estar vazio ou nulo.");            
        }

        public void DefinirLink(string link)
        {
            ValidarLink(link);
            Link = link;
        }

        private void ValidarLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link)) 
                throw new ArgumentNullException(nameof(link), "O Link da rede social não pode estar vazio ou nulo.");
        }
    }
}

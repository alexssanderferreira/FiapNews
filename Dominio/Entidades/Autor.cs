using Dominio.Enum;
using Dominio.ObjetosDeValor;

namespace Dominio.Entidades
{
    public class Autor : Usuario
    {
        protected Autor()
        {

        }
        public Autor(string nome, string login, string senha, string email, string foto, string descricao, List<RedeSocial> redesSociais = null)
            : base(nome, login, senha, email, foto, TipoUsuario.AUTOR)
        {
            DefinirDescricao(descricao);
            AdicionarRedesSociais(redesSociais);
        }

        public void DefinirDescricao(string descricao)
        {
            if (!AutorEhValido(descricao))
                throw new ArgumentException(string.Join("\n", _erros));

            Descricao = descricao.Trim();
        }

        private void AdicionarRedesSociais(List<RedeSocial> redesSociais)
        {
            if (redesSociais == null) return;

            redesSociais.ForEach(AdicionarRedeSocial);
        }

        private List<RedeSocial> _redesSociais;
        public IReadOnlyCollection<RedeSocial> RedesSociais { get => _redesSociais; }
        public string Descricao { get; private set; }
        public virtual IReadOnlyCollection<Noticia> Noticias { get; private set; }

        public void AdicionarRedeSocial(RedeSocial redeSocial)
        {
            if (redeSocial == null) throw new ArgumentNullException();

            if (_redesSociais == null)
                _redesSociais = new List<RedeSocial>();

            if (_redesSociais.Contains(redeSocial))
                throw new ArgumentException(nameof(redeSocial), "Rede social já existe");

            _redesSociais.Add(redeSocial);
        }

        public void RemoverRedeSocial(RedeSocial redeSocial)
        {
            if (!_redesSociais.Contains(redeSocial))
                throw new ArgumentException(nameof(redeSocial),"Rede social não encontrada");

            _redesSociais.Remove(redeSocial);
        }

        private bool AutorEhValido(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                _erros.Add("A descrição do autor não pode estar vazio ou nula.");
            }

            return !_erros.Any();
        }

    }
}

using Dominio.Enum;

namespace Dominio.Entidades
{
    public class Assinante : Usuario
    {
        protected Assinante()
        {

        }

        public Assinante(string nome, string login, string senha, string email, string foto, Assinatura assinatura = null)
            : base(nome, login, senha, email, foto, TipoUsuario.ASSINANTE)
        {
            DefinirAssinatura(assinatura ?? new Assinatura(TipoAssinatura.BASICO));
        }
        
        public Assinatura Assinatura { get; private set; }
        public override bool PodeComentar { get => Assinatura.PodeComentar; }

        public void DefinirAssinatura(Assinatura assinatura)
        {
            Assinatura = assinatura ?? throw new ArgumentNullException(nameof(assinatura), "Valor nulo!");
        }
    }
}

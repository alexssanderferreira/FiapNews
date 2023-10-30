using Dominio.Enum;

namespace Dominio.Entidades
{
    public class Assinatura : Base
    {
        public TipoAssinatura TipoAssinatura { get; private set; }
        public double Preco { get; private set; }
        public int TipoPlano { get; private set; }
        public bool PodeComentar { get => UsuarioPodeComentar(); }

        protected Assinatura()
        {
                
        }

        public Assinatura(TipoAssinatura tipoAssinatura) : base()
        {
            AdicionaAssinatura(tipoAssinatura);
        }

        public Assinatura(TipoAssinatura tipoAssinatura, double preco, int tipoPlano)
        {
            TipoAssinatura = tipoAssinatura;
            CalculaPreco(preco);
            CalculaPeriodicidade(tipoPlano);
        }


        private void AdicionaAssinatura(TipoAssinatura tipoAssinatura)
        {
            TipoAssinatura = tipoAssinatura;
            CalculaPreco();
            CalculaPeriodicidade();
        }

        public void CalculaPreco(double preco = 0)
        {
            switch (TipoAssinatura)
            {
                case TipoAssinatura.BASICO: Preco = 0; break;
                case TipoAssinatura.PAGO: Preco = 50; break;
            }
            if(preco != 0) Preco = preco;
        }

        public void CalculaPeriodicidade(int tipoPlano = 0)
        {
            switch (TipoAssinatura) 
            {
                case TipoAssinatura.BASICO: TipoPlano = 360; break;
                case TipoAssinatura.PAGO:   TipoPlano = 30; break;
            }
            if (tipoPlano != 0) TipoPlano = tipoPlano;
        }

        public void AlterarTipoAssinatura(TipoAssinatura tipoAssinatura)
        {
            TipoAssinatura = tipoAssinatura;
        }

        private bool UsuarioPodeComentar()
        {
            if (TipoAssinatura == TipoAssinatura.PAGO) return true;
            else return false;
        }
    }
}

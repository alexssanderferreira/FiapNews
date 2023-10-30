
using Dominio.Enum;

namespace Dominio.Entidades
{
    public class Administrador : Usuario
    {
        protected Administrador()
        {

        }
        public Administrador(string nome, string login, string senha, string email, string foto)
            : base(nome, login, senha, email, foto, TipoUsuario.ADMINISTRADOR)
        {

        }
    }
}


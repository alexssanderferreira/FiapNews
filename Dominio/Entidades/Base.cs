namespace Dominio.Entidades
{
    public abstract class Base
    {
        public Guid Id { get; private set; }

        public Base()
        {
            Id = Guid.NewGuid();
        }
    }
}

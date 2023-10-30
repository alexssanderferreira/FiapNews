using Aplicacao.Contratos.Persistencia;
using Dominio.ObjetosDeValor;
using Infraestrutura.Persistencia;

namespace Infraestrutura.Repositorio;
public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
    public TagRepository(FiapNewsContext dbContext) : base(dbContext)
    {
    }
    public Tag ObterPorTexto(string texto) => ObterIQueryable().Where(t => t.Texto == texto).FirstOrDefault();
    
}

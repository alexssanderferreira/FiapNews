using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Dominio.ObjetosDeValor;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio
{
    public class NoticiaRepository : RepositoryBase<Noticia>, INoticiaRepository
    {
        public NoticiaRepository(FiapNewsContext dbContext) : base(dbContext)
        {
            
        }

        public override async Task<Noticia> ObterPorIdAsync(Guid id)
        {
            return await _dbContext.Noticias
                                   .Include(n => n.Autores)
                                   .Include(n => n.Categorias)
                                   .FirstOrDefaultAsync(n => n.Id == id); 
        }

        public IEnumerable<Noticia> ObterNoticiaCategoria(Guid idCategoria)
        {
            return _dbContext.Categorias.Include(c => c.Noticias).ToListAsync().Result.Where(c => c.CompararId(idCategoria)).First().Noticias;
        }
    }
}

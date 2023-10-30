using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio
{
    public class AutorRepository : RepositoryBase<Autor>, IAutorRepository
    {
        public AutorRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
        public async Task<IReadOnlyList<Autor>> ObterAutores()
        {
            var autores = await _dbSet.Include(x => x.RedesSociais).ToListAsync();
            return autores;
        }
        public async Task<Autor> ObterAutorPorId(Guid id)
        {
            return await _dbSet.Include(x => x.RedesSociais).FirstOrDefaultAsync(x => x.Id == id);                                                            
        }        
    }
}

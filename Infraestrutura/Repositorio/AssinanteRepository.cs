using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio
{
    public class AssinanteRepository : RepositoryBase<Assinante>, IAssinanteRepository
    {
        public AssinanteRepository(FiapNewsContext dbContext) : base(dbContext)
        {
        }
        public async Task<IReadOnlyList<Assinante>> ObterAssinantes()
        {
            var assinantes = await _dbSet
                .Include(x => x.Assinatura)
                .ToListAsync();
            return assinantes;
        }
        public async Task<Assinante> ObterAssinantePorId(Guid id)
        {
            return await _dbSet
                .Include(x => x.Assinatura)
                .FirstOrDefaultAsync(x => x.Id == id);                                                            
        }        
    }

}

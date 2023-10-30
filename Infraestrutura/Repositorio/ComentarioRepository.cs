using Aplicacao.Contratos.Persistencia;
using Dominio.Entidades;
using Dominio.Enum;
using Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio;

public class ComentarioRepository : RepositoryBase<Comentario>, IComentarioRepository
{
    public ComentarioRepository(FiapNewsContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Comentario> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.Where(x => x.Id == id)
                           .Include(x => x.Noticia)
                           .Include(x => x.Usuario)
                           .Include(x => x.ModeradorResponsavel)
                           .FirstOrDefaultAsync();
    }

    public override async Task<IReadOnlyList<Comentario>> ObterTodosAsync()
    {
        return await _dbSet.Include(x => x.Noticia)
                           .Include(x => x.Usuario)
                           .Include(x => x.ModeradorResponsavel)
                           .ToListAsync();
    }

    public async Task<IEnumerable<Comentario>> GetComentario(EstadoValidacaoComentario estadoValidacao)
    {
        return await _dbSet
            .Where(x => x.EstadoValidacao.Equals(estadoValidacao))
            .Include(x => x.Noticia)
            .Include(x => x.Usuario)
            .Include(x => x.ModeradorResponsavel)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comentario>> GetComentarioPorNoticia(Guid idNoticia, EstadoValidacaoComentario estadoValidacao)
    {
        return await _dbSet
            .Where(x => x.Noticia.Id.Equals(idNoticia) && x.EstadoValidacao == estadoValidacao)
            .Include(x => x.Noticia)
            .Include(x => x.Usuario)
            .Include(x => x.ModeradorResponsavel)
            .ToListAsync();
    }

    public override async Task AdicionarAsync(Comentario comentario)
    {
        await _dbSet.AddAsync(comentario);
        await _dbContext.SaveChangesAsync();
    }
}

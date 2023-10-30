using Dominio.Entidades;
using Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Persistencia
{
    public class FiapNewsContext : DbContext
    {
        public FiapNewsContext(DbContextOptions<FiapNewsContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FiapNewsContext).Assembly);
        }
        
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Assinante> Assinantes { get; set; }
        public DbSet<Assinatura> Assinatura { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

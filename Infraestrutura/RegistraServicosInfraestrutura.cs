using Aplicacao;
using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Infraestrutura.Persistencia;
using Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestrutura
{
    public static class RegistraServicosInfraestrutura
    {
        public static IServiceCollection AddServicosInfraEstrutura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FiapNewsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(IAutorRepository), typeof(AutorRepository));
            services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();
            services.AddScoped(typeof(IAssinanteRepository), typeof(AssinanteRepository));
            services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<INoticiaRepository, NoticiaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped(typeof(IComentarioRepository), typeof(ComentarioRepository));
            services.AddScoped(typeof(INoticiaRepository), typeof(NoticiaRepository));
            services.AddScoped(typeof(IAdministradorRepository), typeof(AdministradorRepository));

            return services;
        }


        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<FiapNewsContext>();

                dbContext.Database.Migrate();

                FiapNewsSeed.Seed(dbContext);
            }
        }
    }
}

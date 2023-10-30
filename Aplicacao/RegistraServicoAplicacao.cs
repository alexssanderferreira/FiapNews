using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplicacao
{
    public static class RegistraServicoAplicacao
    {
        public static IServiceCollection AddServicosAplicacao(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IRedeSocialService, RedeSocialService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IAssinanteService, AssinanteService>();
            services.AddScoped<IAssinaturaService, AssinaturaService>();
            services.AddScoped(typeof(IUsuarioService<>), typeof(UsuarioService<>));            
            services.AddScoped<IComentarioService, ComentarioService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INoticiaService, NoticiaService>();
            services.AddScoped<IAssinanteService, AssinanteService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

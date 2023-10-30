using AutoMapper;
using Dominio.ObjetosDeValor;
using Dominio.Entidades;
using Aplicacao.DTOs.Comentario;
using Aplicacao.DTOs.Administrador;
using Aplicacao.DTOs.Assinante;
using Aplicacao.DTOs.Assinatura;
using Aplicacao.DTOs.Autor;
using Aplicacao.DTOs.Categoria;
using Aplicacao.DTOs.Noticia;
using Aplicacao.DTOs.RedeSocial;
using Aplicacao.DTOs.Tag;
using Aplicacao.DTOs;

namespace Aplicacao.Mapeamento
{
    public class FiapNewsProfile : Profile
    {
        public FiapNewsProfile()
        {
            CreateMap<CategoriaDto, Categoria>();
            CreateMap<Categoria, CategoriaRetornoDto>();

            CreateMap<RedeSocialDto, RedeSocial>();
            CreateMap<RedeSocial, RedeSocialRetornoDto>();

            CreateMap<AutorDto, Autor>();
            CreateMap<Autor, AutorRetornoDto>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.MapFrom(d => d.Email.EnderecoEmail));

            CreateMap<AssinanteDto, Assinante>();
            CreateMap<Assinante, AssinanteRetornoDto>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.MapFrom(d => d.Email.EnderecoEmail));

            CreateMap<AdministradorDto, Administrador>();
            CreateMap<Administrador, AdministradorRetornoDto>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.MapFrom(d => d.Email.EnderecoEmail));

            CreateMap<TagDto, Tag>();
            CreateMap<Tag, TagRetornoDto>();

            CreateMap<NoticiaDto, Noticia>();
            CreateMap<Noticia, NoticiaRetornoDto>();

            CreateMap<ComentarioDto, Comentario>();
            CreateMap<Comentario, ComentarioRetornoDto>();

            CreateMap<AssinaturaDto, Assinatura>();
            CreateMap<Assinatura, AssinaturaRetornoDto>();
        }
    }
}

using AutoMapper;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.ViewModel;

namespace Fiap.Api.Donation7
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {

            CreateMap<UsuarioModel, UsuarioResponseVM>();
            CreateMap<UsuarioModel, LoginResponseVM>();

            CreateMap<ProdutoModel, ProdutoResponseVM>()
                .ForMember(
                    dest => dest.NomeCategoria,
                    opt => opt.MapFrom(src => src.Categoria == null ? String.Empty : src.Categoria.NomeCategoria)
                )
                .ForMember(
                    dest => dest.NomeUsuario,
                    opt => opt.MapFrom(src => src.Usuario == null ? String.Empty : src.Usuario.NomeUsuario)
                );

        //CreateMap<UsuarioModel, UsuarioResponseVM>()
        //    .ForMember(dest => dest.UsuarioCodigo, opt => opt.MapFrom(src => src.UsuarioId))
        //    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeUsuario))
        //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailUsuario));



    }

    }
}

using AutoMapper;
using Fiap.Api.Donation7.Model;
using Fiap.Api.Donation7.ViewModel;

namespace Fiap.Api.Donation7
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

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

            CreateMap<TrocaRequestVM, TrocaModel>();

            CreateMap<TrocaModel, TrocaResponseVM>()
               .ForMember(
                   dest => dest.ProdutoNomeEscolhido,
                   opt => opt.MapFrom(src => src.ProdutoEscolhido == null ? String.Empty : src.ProdutoEscolhido.Nome)
               )
               .ForMember(
                   dest => dest.ProdutoNomeMeu,
                   opt => opt.MapFrom(src => src.ProdutoMeu == null ? String.Empty : src.ProdutoMeu.Nome)
               );

        }
    }
}

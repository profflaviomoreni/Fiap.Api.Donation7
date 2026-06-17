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


            //CreateMap<UsuarioModel, UsuarioResponseVM>()
            //    .ForMember(dest => dest.UsuarioCodigo, opt => opt.MapFrom(src => src.UsuarioId))
            //    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.NomeUsuario))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailUsuario));



        }

    }
}

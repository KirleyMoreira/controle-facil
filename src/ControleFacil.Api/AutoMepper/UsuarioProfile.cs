
using AutoMapper;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.AutoMepper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
             CreateMap<Usuario, UsuarioLoginRequestContract>().ReverseMap();
             CreateMap<Usuario, UsuarioLoginResponseContract>().ReverseMap();
        }
    }
}
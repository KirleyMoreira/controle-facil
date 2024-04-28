using ControleFacil.Api.Contract.Usuario;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface IUsuarioServise : IServise<UsuarioLoginRequestContract, UsuarioLoginResponseContract, long>
    {
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequestContract);
        Task<UsuarioResponseContract> Obter(string email);
    }
}
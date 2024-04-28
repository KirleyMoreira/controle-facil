namespace ControleFacil.Api.Contract.Usuario
{
    public class UsuarioResponseContract : UsuarioLoginRequestContract
    {
       public long  Id {get; set;}
       public DateTime DataCadastro {get; set;}
    }
}
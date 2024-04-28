using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Classes;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class UsuarioServise : IUsuarioServise
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly TokenServise _tokenServise;
        public UsuarioServise(
             IUsuarioRepository usuarioRepository, 
             IMapper mapper,
             TokenServise tokenServise )
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenServise = tokenServise;
        }

        
        public async Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequestContract)
        {
            var usuario = await Obter(usuarioLoginRequestContract.Email);

            var hashSenha = GerarHashSenha(usuarioLoginRequestContract.Senha);

            if(usuario is null || usuario.Senha != hashSenha)
            {
                throw new AuthenticationException(" Usuário ou senha inválido. ");
            }

            return new UsuarioLoginResponseContract{
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenServise.GerarToken(_mapper.Map<Usuario>(usuario))
            };
           
        }
        public async Task<UsuarioLoginResponseContract> Adicionar(UsuarioLoginRequestContract entidade, long idUsuario)
        {
            
            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.Senha = GerarHashSenha(usuario.Senha);

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioLoginResponseContract>(usuario);

        }

        private string GerarHashSenha(string senha)
        {
            string hashSenha;

            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
                byte[] bytesHashSenha = sha256.ComputeHash(bytesSenha);
                hashSenha = BitConverter.ToString(bytesHashSenha).Replace("-", "").Replace("-", "").ToLower();
            }

            return hashSenha;

        }
        
         public async Task<UsuarioLoginResponseContract> Atualizar(long id, UsuarioLoginRequestContract entidade, long idUsuario)
        {
            _ =  await Obter(id) ?? throw new Exception(" Usuaário não encontrado para atualização.");

            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.Id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);

            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioLoginResponseContract>(usuario);
        }

        public async Task<IEnumerable<UsuarioLoginResponseContract>> Obter(long idUsuario)
        {
            var usuarios = await _usuarioRepository.Obter();

            return usuarios.Select(Usuario => _mapper.Map<UsuarioLoginResponseContract>(Usuario));
        }

        public async Task<UsuarioLoginResponseContract> Obter(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.Obter(id);
            return _mapper.Map<UsuarioLoginResponseContract>(usuario);
        }

        public async Task<UsuarioResponseContract> Obter(string email)
        {
            var usuario = await _usuarioRepository.Obter(email);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var usuario =  await Obter(id) 
            ?? throw new Exception(" Usuário não encontrado para inativação. ");

            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));
        }

        Task<UsuarioLoginResponseContract> IServise<UsuarioLoginRequestContract, UsuarioLoginResponseContract, long>.Inativar(long id, long idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
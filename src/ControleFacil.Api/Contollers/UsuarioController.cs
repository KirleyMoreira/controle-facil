using System.Security.Authentication;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Contollers
{

    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServise _usuarioServise;
        public UsuarioController(IUsuarioServise usuarioServise)
        {
            _usuarioServise = usuarioServise;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]

        public async Task<IActionResult> Autenticar(UsuarioLoginRequestContract contrato)
        {
            try
            {
                return Ok (await _usuarioServise.Autenticar(contrato));
            }
            catch(AuthenticationException ex)
            {
                return  Unauthorized(new { StatusCode = 401, message = ex.Message});
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        // [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> Adicionar(UsuarioRequestContract contrato)
        {
            try
            {
                return Created("", await _usuarioServise.Adicionar(contrato, 0));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Obter()
        {
            try
            {
                return Ok(await _usuarioServise.Obter(0));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]

        public async Task<IActionResult> Obter(long id)
        {
            try
            {
                return Ok(await _usuarioServise.Obter(id, 0));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route ("{id}")]
        [Authorize]

        public async Task<IActionResult> Atualizar(long id, UsuarioRequestContract contrato)
        {
            try
            {
                return Ok( await _usuarioServise.Atualizar(id, contrato, 0));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _usuarioServise.Inativar(id, 0);
                return NoContent();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        
    }
}
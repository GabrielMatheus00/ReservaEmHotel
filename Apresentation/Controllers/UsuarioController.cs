using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaHotel.Domain.Model.DTOs.Usuario;
using ReservaHotel.Services.Services;
using ReservaHotel.Services.Services.Interfaces;

namespace ReservaHotel.Apresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login(LoginDTO dto)
        {
            var response = _usuarioService.Login(dto);
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Cadastro")]
        public async Task<IActionResult> Cadastro(CadastroUsuarioDTO dto)
        {
            var response = await _usuarioService.CadastroUsuario(dto);
            return Ok(response);
        }
    }
}

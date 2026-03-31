using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReservaHotel.Domain.Model.DTOs.Hotel;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Services.Services.Interfaces;

namespace ReservaHotel.Apresentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController:Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IQuartoService _quartoService;
        public HotelController(IHotelService hotelService, IQuartoService quartoService)
        {
            _hotelService = hotelService;
            _quartoService = quartoService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastra(AddHotelDTO dto)
        {
            var response = await _hotelService.Adiciona(dto);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet]
        public IActionResult Busca()
        {
            var response = _hotelService.Busca();
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public IActionResult Busca(Guid id)
        {
            var response = _hotelService.Busca(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var response = await _hotelService.Remove(id);
            if (response.Success)
                return NoContent();
            return NotFound();
        }

        [HttpPatch]
        public async Task<IActionResult> Edita(UpdateHotelDTO dto)
        {
            var response = await _hotelService.Edita(dto);
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("quarto")]
        public async Task<IActionResult> CadastraQuarto(AddQuartoDTO dto)
        {
            var response = await _quartoService.CadastraQuarto(dto);
            if (response.Success)
                return CreatedAtAction(nameof(CadastraQuarto), response.Data);
            return BadRequest(response);
        }

        [HttpPatch("quarto")]
        public async Task<IActionResult> AtualizaQuarto(UpdateQuartoDTO dto) 
        {
            var response = await _quartoService.EditaQuarto(dto);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("quarto/{id}")]
        public async Task<IActionResult> RemoveQuarto(Guid id)
        {
            var response = await _quartoService.RemoveQuarto(id);
            if(response.Success)    
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("quarto/{id}")]
        public IActionResult BuscaQuarto(Guid id)
        {
            var response = _quartoService.BuscaQuarto(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("hotel/quartos/{hotelId}")]
        public IActionResult BuscaQuartosPorHotel(Guid hotelId)
        {
            var response = _quartoService.BuscaQuartosPorHotel(hotelId);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}

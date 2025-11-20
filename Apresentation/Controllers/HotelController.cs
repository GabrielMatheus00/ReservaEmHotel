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
        public IActionResult CadastraHotel(AddHotelDTO dto)
        {
            var response = _hotelService.AdicionaHotel(dto);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet]
        public IActionResult BuscarHoteis()
        {
            var response = _hotelService.BuscaHoteis();
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public IActionResult BuscaHotel(Guid id)
        {
            var response = _hotelService.BuscaHotel(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveHotel(Guid id)
        {
            var response = _hotelService.RemoveHotel(id);
            if (response.Success)
                return NoContent();
            return NotFound();
        }

        [HttpPut]
        public IActionResult EditaHotel(UpdateHotelDTO dto)
        {
            var response = _hotelService.EditaHotel(dto);
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("quarto")]
        public IActionResult CadastraQuarto(AddUpdateQuartoDTO dto)
        {
            var response = _quartoService.CadastraQuarto(dto);
            if (response.Success)
                return CreatedAtAction(nameof(CadastraQuarto), response.Data);
            return BadRequest(response);
        }

        [HttpPatch("quarto")]
        public IActionResult AtualizaQuarto(AddUpdateQuartoDTO dto) 
        {
            var response = _quartoService.EditaQuarto(dto);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("quarto/{id}")]
        public IActionResult RemoveQuarto(Guid id)
        {
            var response = _quartoService.RemoveQuarto(id);
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

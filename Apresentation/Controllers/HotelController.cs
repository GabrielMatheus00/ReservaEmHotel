using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReservaHotel.Domain.Model.DTOs;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using ReservaHotel.Services.Services.Interfaces;

namespace ReservaHotel.Apresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController:ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService =  hotelService;
        }

        [HttpPost("/cadastro")]
        public IActionResult CadastraHotel(AddUpdateHotelDTO dto)
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

        [HttpGet("/{id}")]
        public IActionResult BuscaHotel(Guid id)
        {
            var response = _hotelService.BuscaHotel(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("/{id}")]
        public IActionResult RemoveHotel(Guid id)
        {
            var response = _hotelService.RemoveHotel(id);
            if (response.Success)
                return NoContent();
            return NotFound();
        }

        [HttpPut("/atualizacao")]
        public IActionResult AtualizaHotel(AddUpdateHotelDTO dto)
        {
            var response = _hotelService.EditaHotel(dto);
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("/quarto")]
        public IActionResult CadastraQuarto(AddUpdateQuartoDTO dto)
        {
            var response = _hotelService.AdicionaQuarto(dto);
            if (response.Success)
                return CreatedAtAction(nameof(CadastraQuarto), response.Data);
            return BadRequest(response);
        }

        [HttpPatch("/quarto")]
        public IActionResult AtualizaQuarto(AddUpdateQuartoDTO dto) 
        {
            var response = _hotelService.EditaQuarto(dto);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("/quarto/{id}")]
        public IActionResult RemoveQuarto(Guid id)
        {
            var response = _hotelService.RemoveQuarto(id);
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("/quarto/{id}")]
        public IActionResult BuscaQuarto(Guid id)
        {
            var response = _hotelService.BuscaQuarto(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("/hotel/quartos/{hotelId}")]
        public IActionResult BuscaQuartosPorHotel(Guid hotelId)
        {
            var response = _hotelService.BuscaQuartosPorHotel(hotelId);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Model.DTOs.Hotel;

public class UpdateHotelDTO
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public int? Estrelas { get; set; }
    public int? Andares { get; set; }
}

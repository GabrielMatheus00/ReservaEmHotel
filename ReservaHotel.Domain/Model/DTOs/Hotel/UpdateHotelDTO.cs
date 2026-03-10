using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Model.DTOs.Hotel;

public record UpdateHotelDTO
{
    public Guid Id { get;}
    public string? Nome { get;}
    public int? Estrelas { get; }
    public int? Andares { get; }
}

using ReservaHotel.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Model.DTOs.Quarto
{
    public record UpdateQuartoDTO
    {
        public Guid Id { get; init; }
        public int? Numero { get; init; }
        public float? Tamanho { get; init; }
        public int? Andar { get; init; }
        public string Ocupacao { get; init; }
        public Guid? HotelId { get; init;  }
        public TipoQuarto? TipoQuarto { get; init; }
        public float? DiariaDolar { get; init; }
    }
}

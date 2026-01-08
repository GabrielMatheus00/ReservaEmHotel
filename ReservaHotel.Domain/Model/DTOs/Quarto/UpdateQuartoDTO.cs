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
        public Guid Id { get; }
        public int? Numero { get; }
        public float? Tamanho { get; }
        public int? Andar { get; }
        public string Ocupacao { get; }
        public Guid HotelId { get; }
        public TipoQuarto? TipoQuarto { get;}
        public float? DiariaDolar { get;}
    }
}

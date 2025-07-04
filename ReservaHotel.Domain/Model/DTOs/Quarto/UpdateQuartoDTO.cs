using ReservaHotel.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Model.DTOs.Quarto
{
    public class UpdateQuartoDTO
    {
        public int Numero { get; set; }
        public float Tamanho { get; set; }
        public int Andar { get; set; }
        public string Ocupacao { get; set; }
        public Guid HotelId { get; set; }
        public TipoQuarto TipoQuarto { get; set; }
        public float DiariaDolar { get; set; }
    }
}

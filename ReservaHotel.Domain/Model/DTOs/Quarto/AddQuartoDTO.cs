
using ReservaHotel.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Model.DTOs.Quarto
{
    public record AddQuartoDTO(
        int Numero,
        float Tamanho,
        int Andar,
        string Ocupacao,
        Guid HotelId,
        TipoQuarto TipoQuarto,
        decimal DiariaDolar);
    
}

using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services.Services.Interfaces
{
    public interface IHotelService
    {
        public ResponseBase<Guid> AdicionaHotel(AddUpdateHotelDTO hotel);
        public ResponseBase<string> EditaHotel(AddUpdateHotelDTO hotel);
        public ResponseBase<Guid> RemoveHotel(Guid id);
        public ResponseBase<Hotel> BuscaHotel(Guid id);

        public ResponseBase<Guid> AdicionaQuarto(AddUpdateQuartoDTO dto);

        public ResponseBase<List<Hotel>> BuscaHoteis();
        public ResponseBase<string> EditaQuarto(AddUpdateQuartoDTO dto);
        public ResponseBase<Guid> RemoveQuarto(Guid id);
        public ResponseBase<Quarto> BuscaQuarto(Guid id);
        public ResponseBase<List<Quarto>> BuscaQuartosPorHotel(Guid id);
    }
}

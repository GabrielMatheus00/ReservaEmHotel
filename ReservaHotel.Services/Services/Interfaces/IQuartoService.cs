using ReservaHotel.Data.Database;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Quarto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services.Services.Interfaces
{
    public interface IQuartoService
    {
        public Task<ResponseBase<Guid>> CadastraQuarto(AddQuartoDTO dto);
        public Task<ResponseBase<Guid>> EditaQuarto(UpdateQuartoDTO dto);
        public Task<ResponseBase<Guid>> RemoveQuarto(Guid id);
        public ResponseBase<Quarto> BuscaQuarto(Guid id);

        public ResponseBase<List<Quarto>> BuscaQuartosPorHotel(Guid hotelId);
    }
}

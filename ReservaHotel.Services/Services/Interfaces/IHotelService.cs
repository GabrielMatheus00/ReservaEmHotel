using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Hotel;
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
        public ResponseBase<Guid> Adiciona(AddHotelDTO hotel);
        public ResponseBase<string> Edita(UpdateHotelDTO hotel);
        public ResponseBase<Guid> Remove(Guid id);
        public ResponseBase<Hotel> Busca(Guid id);
        public ResponseBase<List<Hotel>> Busca();

    }
}

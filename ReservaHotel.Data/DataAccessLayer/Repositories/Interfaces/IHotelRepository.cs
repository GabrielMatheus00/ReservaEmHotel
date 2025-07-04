using ReservaHotel.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces
{
    public interface IHotelRepository:IRepositoryBase<Hotel>
    {
        public Hotel BuscaHotelComQuartos(Guid hotelId);
    }
}

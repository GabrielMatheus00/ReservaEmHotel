using Microsoft.EntityFrameworkCore;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Data.Database;
using ReservaHotel.Data.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer.Repositories.Classes
{
    public class HotelRepository : RepositoryBase<Hotel>,IHotelRepository
    {
        private readonly HotelDbContext _dbContext ;

        public HotelRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ;
        }

        public Hotel BuscaHotelComQuartos(Guid hotelId)
        {
            var hotel = _dbContext.Set<Hotel>().Include(a => a.Quartos).FirstOrDefault(h => h.Ativo && h.Id == hotelId);
            return hotel;
        }

    }
}

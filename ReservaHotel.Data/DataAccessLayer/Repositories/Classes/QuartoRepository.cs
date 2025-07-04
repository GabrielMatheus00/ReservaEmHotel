using Microsoft.EntityFrameworkCore;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer.Repositories.Classes
{
    public class QuartoRepository : RepositoryBase<Quarto>, IQuartoRepository
    {
        public QuartoRepository(HotelDbContext dbContext) : base(dbContext)
        {

        }
    }
}

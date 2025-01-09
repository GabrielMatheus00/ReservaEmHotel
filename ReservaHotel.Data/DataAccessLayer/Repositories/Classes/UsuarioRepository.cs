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
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}

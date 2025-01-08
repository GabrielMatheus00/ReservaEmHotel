using Microsoft.EntityFrameworkCore;
using ReservaHotel.Data.DataAccessLayer.Repositories.Classes;
using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using ReservaHotel.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _dbContext;
        public UnitOfWork(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
            HotelRepository = new HotelRepository(dbContext);
            QuartoRepository = new QuartoRepository(dbContext);
        }
        public IHotelRepository HotelRepository { get; }
        public IQuartoRepository QuartoRepository { get; }
        public void SalvarAlteracoes()
        {
            _dbContext.SaveChanges();
        }
    }
}

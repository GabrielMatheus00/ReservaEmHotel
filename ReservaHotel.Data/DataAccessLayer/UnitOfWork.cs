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
            UsuarioRepository = new UsuarioRepository(dbContext);
            CotacaoMoedaRepository = new CotacaoMoedaRepository(dbContext);
        }
        public IHotelRepository HotelRepository { get; }
        public IQuartoRepository QuartoRepository { get; }

        public IUsuarioRepository UsuarioRepository { get; }

        
        public ICotacaoMoedaRepository CotacaoMoedaRepository { get; }
        public async Task SalvarAlteracoes()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

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
    public class CotacaoMoedaRepository : RepositoryBase<CotacaoMoeda>, ICotacaoMoedaRepository
    {
        private readonly HotelDbContext _dbContext;
        public CotacaoMoedaRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public CotacaoMoeda BuscaCotacaoMaisRecente()
        {
            return _dbContext.Set<CotacaoMoeda>().OrderByDescending(a => a.DataCotacao).FirstOrDefault(a => a.Ativo);
        }
    }
}

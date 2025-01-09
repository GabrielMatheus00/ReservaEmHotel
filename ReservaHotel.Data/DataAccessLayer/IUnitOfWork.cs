using ReservaHotel.Data.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.DataAccessLayer
{
    public interface IUnitOfWork
    {
        IHotelRepository HotelRepository { get; }
        IQuartoRepository QuartoRepository { get; }

        IUsuarioRepository UsuarioRepository { get; }
        void SalvarAlteracoes();
    }
}

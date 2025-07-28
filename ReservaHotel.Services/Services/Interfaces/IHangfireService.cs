using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Extensions.Hangfire
{
    public interface IHangfireService
    {
        public Task AtualizaValorDolar();
        public Task AtualizaPrecoQuartos();


    }
}

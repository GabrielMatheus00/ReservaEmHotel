using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Response
{
    public class ConsultaValorDolar
    {
        public decimal CotacaoCompra { get; set; }
        public decimal CotacaoVenda { get; set; }
        public string DataHoraCotacao { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Domain.Configuration
{
    public class AppConfig
    {
        public string UrlBACEN { get; set; }
        public string ChavePrivadaJwt { get; set; }

        public string HorasExpiracaoToken { get; set; }
    }
}

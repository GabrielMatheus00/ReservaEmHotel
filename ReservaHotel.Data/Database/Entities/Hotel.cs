using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.Database.Entities
{
    public class Hotel : BaseEntity
    {

        public string Nome { get; set; }

        public int NumeroQuartos { get; set; }

        public int Estrelas { get; set; }


        public int Andares { get; set; }

        public ICollection<Quarto> Quartos { get; set; }
    }
}

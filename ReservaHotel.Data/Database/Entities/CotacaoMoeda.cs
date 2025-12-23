using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Data.Database.Entities
{
    public class CotacaoMoeda:BaseEntity
    {
        public string Moeda { get; set; }
        public decimal CotacaoCompra { get; set; }
        public decimal? CotacaoVenda { get; set; }
        public DateTime DataCotacao { get; set; }
        public ICollection<Quarto> Quartos { get; set; }

        public CotacaoMoeda(string moeda, DateTime dataCotacao, decimal cotacaoCompra, decimal? cotacaoVenda = null)
        {
            Moeda = moeda;
            CotacaoVenda = cotacaoVenda;
            CotacaoCompra = cotacaoCompra;
            DataCotacao = dataCotacao;
            Ativo = true;
            Id = Guid.NewGuid();
        }
    }
}

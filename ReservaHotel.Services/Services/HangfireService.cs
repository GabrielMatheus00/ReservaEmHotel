using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Domain.Configuration;
using ReservaHotel.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Extensions.Extensions.Hangfire
{
    public class HangfireService : IHangfireService
    {
        private readonly string _UrlBACEN;
        private readonly AppConfig _config;
        private readonly IUnitOfWork _unitOfWork;
        public HangfireService(IOptions<AppConfig> config, IUnitOfWork unitOfWork)
        {
            _config = config.Value;
            _UrlBACEN = _config.UrlBACEN;
            _unitOfWork = unitOfWork;
        }
        public async Task AtualizaValorDolar()
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_UrlBACEN);
                string data = DateTime.Now.AddDays(-1).ToString("MM-dd-yyyy");
                var requisicao = await httpClient.GetAsync($"CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{data}'&$top=1&$format=json");
                if (requisicao is null || !requisicao.IsSuccessStatusCode) throw new Exception("Não foi possível conectar a API da Bacen");
                var resposta = await requisicao.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(resposta);
                string valores = Convert.ToString(obj["value"][0]);
                if (!string.IsNullOrEmpty(valores))
                {
                    ConsultaValorDolar resultado = JsonConvert.DeserializeObject<ConsultaValorDolar>(valores);
                    DateTime dataCotacao = Convert.ToDateTime(resultado.DataHoraCotacao);
                    CotacaoMoeda cotacaoMaisRecente = _unitOfWork.CotacaoMoedaRepository.BuscaCotacaoMaisRecente();
                    if (cotacaoMaisRecente is not null && cotacaoMaisRecente.DataCotacao == dataCotacao)
                        return;
                    CotacaoMoeda cotacao = new CotacaoMoeda("Dolar", dataCotacao, resultado.CotacaoCompra, resultado.CotacaoVenda);
                    _unitOfWork.CotacaoMoedaRepository.Adicionar(cotacao);
                    _unitOfWork.SalvarAlteracoes();
                    return;

                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}

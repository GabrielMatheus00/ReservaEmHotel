
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.Database;
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
        private readonly ILogger<HangfireService> _logger;
        public HangfireService(IOptions<AppConfig> config, IUnitOfWork unitOfWork, ILogger<HangfireService> logger)
        {
            _config = config.Value;
            _UrlBACEN = _config.UrlBACEN;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task AtualizaValorDolar()
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_UrlBACEN);
                string data = DateTime.Now.ToString("MM-dd-yyyy");
                throw new ArgumentException("Teste do gabriel", "inner exception");
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
                    await _unitOfWork.SalvarAlteracoes();
                    BackgroundJob.Enqueue(() => AtualizaPrecoQuartos());
                    return;

                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                _logger.LogInformation("aloo");
            }
        }
        public async Task AtualizaPrecoQuartos()
        {
            try
            {
                int skip = 0;
                int take = 50;
                bool temMaisQuartosParaAtualizacao = true;
                List<Quarto> quartos;
                CotacaoMoeda ultimaCotacao = _unitOfWork.CotacaoMoedaRepository.BuscaCotacaoMaisRecente();
                if (ultimaCotacao is null)
                    return;
                while (temMaisQuartosParaAtualizacao)
                {
                    quartos = _unitOfWork.QuartoRepository.BuscarPaginado(q => q.Ativo && (q.CotacaoId == null || q.CotacaoId != ultimaCotacao.Id), skip, take);
                    if (quartos is null || !quartos.Any())
                        return;
                    quartos.ForEach(q =>
                    {
                        q.DiariaReal = ultimaCotacao.CotacaoVenda.Value * q.DiariaDolar;
                        q.CotacaoId = ultimaCotacao.Id;
                        _unitOfWork.QuartoRepository.Atualizar(q);
                    });
                    await _unitOfWork.SalvarAlteracoes();
                    temMaisQuartosParaAtualizacao = quartos.Count == take;
                    skip += take;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
            }
        }
    }
}

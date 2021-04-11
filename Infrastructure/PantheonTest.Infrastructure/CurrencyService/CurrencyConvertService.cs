using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Application.Models;

namespace PantheonTest.Infrastructure.CurrencyService
{
    public class CurrencyConvertService : ICurrencyConvertService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<CurrencyConvertService> _logger;
        public CurrencyConvertApiSettings ApiSettings { get; }

        public CurrencyConvertService(IOptions<CurrencyConvertApiSettings> apiSettings, IHttpClientFactory clientFactory, ILogger<CurrencyConvertService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            ApiSettings = apiSettings.Value;
        }

        public async Task<decimal> GetCurrencyConvert(string currencySymbol)
        {
            var url = $"{ApiSettings.ApiUrl}?base={currencySymbol}&symbols=GBP";
            decimal convertedValue = 1; // 
            try
            {
                var client = _clientFactory.CreateClient("RatesApiClient");
               
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);
                    convertedValue = obj.rates.GBP;
                }
                else
                {
                    _logger.LogError(
                        $"Invalid Customer Details: {response.ReasonPhrase} - {JsonConvert.SerializeObject(response)}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Customer service - {ex.Message}");
                throw;
            }

            return convertedValue;
        }
    }
}
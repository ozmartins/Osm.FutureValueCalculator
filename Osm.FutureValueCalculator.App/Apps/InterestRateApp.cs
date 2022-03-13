using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Apps
{
    public class InterestRateApp : IInterestRateApp
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InterestRateApp(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<InterestRateModel> GetInterestRate()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var resp = await httpClient.GetAsync("http://localhost:5001/taxajuros/");

            resp.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<InterestRateModel>(await resp.Content.ReadAsStreamAsync());
        }
    }
}

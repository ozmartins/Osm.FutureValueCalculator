using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System.Net.Http;
using System.Text.Json;

namespace Osm.FutureValueCalculator.App.Apps
{
    public class InterestRateApp : IInterestRateApp
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InterestRateApp(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public InterestRateModel GetInterestRate()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var resp = httpClient.GetAsync("https://localhost:44389/taxajuros").Result;

            resp.EnsureSuccessStatusCode();

            return JsonSerializer.DeserializeAsync<InterestRateModel>(resp.Content.ReadAsStreamAsync().Result).Result;
        }
    }
}

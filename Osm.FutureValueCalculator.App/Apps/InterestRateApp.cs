using Newtonsoft.Json;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System.Net.Http;

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
            HttpClient client = new HttpClient();

            InterestRateModel interestRateModel = null;

            HttpResponseMessage response = await client.GetAsync("https://localhost:44389/taxajuros");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                interestRateModel = JsonConvert.DeserializeObject<InterestRateModel>(content);
            }

            return interestRateModel;
        }        
    }
}

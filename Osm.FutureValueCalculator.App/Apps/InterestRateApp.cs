using Newtonsoft.Json;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System;
using System.Net.Http;

using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Apps
{
    public class InterestRateApp : IInterestRateApp
    {
        private readonly HttpClient _client;

        public InterestRateApp(HttpClient client)
        {
            _client = client;
        }

        public async Task<InterestRateModel> GetInterestRateAsync(string interestRateApiUrl)
        {            
            InterestRateModel interestRateModel = null;            

            HttpResponseMessage response = await _client.GetAsync(interestRateApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                interestRateModel = JsonConvert.DeserializeObject<InterestRateModel>(content);
            }

            return interestRateModel;
        }        
    }
}

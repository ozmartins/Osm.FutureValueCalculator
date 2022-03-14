using Newtonsoft.Json;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.App.Models;
using Osm.FutureValueCalculator.Domain.Models;
using System;
using System.Collections.Generic;
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

        public async Task<GetInterestRateResult> GetInterestRateAsync(string interestRateApiUrl)
        {                       
            HttpResponseMessage response = await _client.GetAsync(interestRateApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return new GetInterestRateResult()
                {
                    Success = true,
                    InterestRateModel = JsonConvert.DeserializeObject<InterestRateModel>(content)
                };
            }
            else
            {
                return new GetInterestRateResult()
                {
                    Success = false,
                    Errors = new List<string> { response.ReasonPhrase }
                };
            }            
        }        
    }
}

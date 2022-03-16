using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Osm.FutureValueCalculator.Api;
using Osm.FutureValueCalculator.App.Models;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.IntegrationTest
{
    [TestClass]
    public class ShowMeTheCodeTest
    {
        private HttpClient _client;
        private TestServer _server;
        private IConfiguration _configuration;

        public ShowMeTheCodeTest()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()));
            
            _client = _server.CreateClient();            
        }

        [TestMethod]
        public async Task GettingValidData()
        {
            #region arrange
            var targetUrl = "/ShowMeTheCode";            
            var expectedGitHubInterestRateUrl = _configuration.GetValue<string>("ShowMeTheCode:GitHub:InterestRateUrl");
            var expectedGitHubFutureValueCalculatorUrl = _configuration.GetValue<string>("ShowMeTheCode:GitHub:FutureValueCalculatorUrl"); ;
            var expectedHerokuInterestRateUrl = _configuration.GetValue<string>("ShowMeTheCode:Heroku:InterestRateUrl"); ;
            var expectedHerokuFutureValueCalculatorUrl = _configuration.GetValue<string>("ShowMeTheCode:Heroku:FutureValueCalculatorUrl");
            #endregion

            #region act
            var responseMessage = await _client.GetAsync(targetUrl);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var showMeTheCodeModel = JsonConvert.DeserializeObject<ShowMeTheCodeModel>(content);
            #endregion

            #region assert
            Assert.IsTrue(responseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(showMeTheCodeModel.GitHub);
            Assert.IsNotNull(showMeTheCodeModel.Heroku);
            Assert.AreEqual(showMeTheCodeModel.GitHub.InterestRateUrl, expectedGitHubInterestRateUrl);
            Assert.AreEqual(showMeTheCodeModel.GitHub.FutureValueCalculatorUrl, expectedGitHubFutureValueCalculatorUrl);
            Assert.AreEqual(showMeTheCodeModel.Heroku.InterestRateUrl, expectedHerokuInterestRateUrl);
            Assert.AreEqual(showMeTheCodeModel.Heroku.FutureValueCalculatorUrl, expectedHerokuFutureValueCalculatorUrl);
            #endregion            
        }
    }
}

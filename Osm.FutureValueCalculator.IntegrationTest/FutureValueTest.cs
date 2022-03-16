using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Osm.FutureValueCalculator.Api;
using Osm.FutureValueCalculator.Api.Controllers;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.IntegrationTest
{

    [TestClass]
    public class FutureValueTest
    {
        public static decimal ExpectedFutureValue = 105.1m;

        public static string  ExpectedErrorMessage = "Test exception";

        [TestMethod]
        public async Task GettingFutureValueWithValidParameters()
        {
            #region arrange            
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(services => { services.AddScoped<IFutureValueApp, MockFutureValueApp>(); }));
            
            var client = server.CreateClient();
            
            var targetUrl = "/CalculaJuros?ValorInicial=100&Meses=5";
            #endregion

            #region act
            var responseMessage = await client.GetAsync(targetUrl);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var futureValueCalcResult = JsonConvert.DeserializeObject<FutureValueCalcResult>(content);
            #endregion

            #region assert
            Assert.IsTrue(responseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(futureValueCalcResult);
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, ExpectedFutureValue);
            #endregion            
        }

        [TestMethod]
        public async Task GettingFutureValueWithException()
        {
            #region arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(services => { services.AddScoped<IFutureValueApp, MockFutureValueAppThatThrownsException>(); }));

            var client = server.CreateClient();

            var targetUrl = "/CalculaJuros?ValorInicial=100&Meses=5";
            #endregion

            #region act
            var responseMessage = await client.GetAsync(targetUrl);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var futureValueCalcResult = JsonConvert.DeserializeObject<FutureValueCalcResult>(content);
            #endregion

            #region assert
            Assert.IsFalse(responseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(futureValueCalcResult);
            Assert.IsFalse(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.FutureValue, 0);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 1);
            Assert.AreEqual(futureValueCalcResult.Errors[0], ExpectedErrorMessage);
            #endregion            
        }
    }

    public class MockFutureValueApp : IFutureValueApp
    {
        public async Task<FutureValueCalcResult> CalculateFutureValueAsync(decimal presentValue, int months)
        {           
            return await Task.Run(() => new FutureValueCalcResult() { Success = true, FutureValue = FutureValueTest.ExpectedFutureValue });
        }
    }

    public class MockFutureValueAppThatThrownsException : IFutureValueApp
    {
        public Task<FutureValueCalcResult> CalculateFutureValueAsync(decimal presentValue, int months)
        {
            throw new Exception(FutureValueTest.ExpectedErrorMessage);
        }
    }
}

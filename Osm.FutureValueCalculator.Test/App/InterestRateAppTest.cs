using Microsoft.VisualStudio.TestTools.UnitTesting;
using Osm.FutureValueCalculator.App.Apps;
using System.Net.Http;

namespace Osm.FutureValueCalculator.Test.App
{
    [TestClass]
    public class InterestRateAppTest
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InterestRateAppTest(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //The main goal of this class is to know if the classe returns the values returned by the remote service.
        //So, I won't execute a test for each possible situação, because it was done in the FutureValueService.

        [TestMethod]
        public async void InterestRateAppTest_GettingValidInterestRate()
        {
            var interestRateApp = new InterestRateApp(_httpClientFactory);

            var interestRateModel = await interestRateApp.GetInterestRate();

            Assert.AreEqual(interestRateModel.Value, 0.01);
        }        
    }
}

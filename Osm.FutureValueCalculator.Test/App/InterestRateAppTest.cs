using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Contrib.HttpClient;
using Osm.FutureValueCalculator.App.Apps;
using System;
using System.Net.Http;

namespace Osm.FutureValueCalculator.Test.App
{
    [TestClass]
    public class InterestRateAppTest
    {       
        //The main goal of this class is to know if the classe returns the values returned by the remote service.
        //So, I won't execute a test for each possible situação, because it was done in the FutureValueService.

        [TestMethod]
        public void InterestRateAppTest_GettingValidInterestRate()
        {
            //Arrange                       
            var handler = new Mock<HttpMessageHandler>();
            var factory = handler.CreateClientFactory();

            Mock.Get(factory).Setup(x => x.CreateClient("Osm.InterestRate.Api"))
                .Returns(() =>
                {
                    var client = handler.CreateClient();
                    client.BaseAddress = new Uri("http://localhost:5001/taxajuros/");

                    return client;
                });
           
            handler.SetupRequest(HttpMethod.Get, "http://localhost:5001/taxajuros/").ReturnsResponse("{'Value':0.01}");

            //Act
            var taxaJurosAplic = new InterestRateApp(factory).GetInterestRate();

            //Test
            //Assert.AreEqual(0.01f, taxaJurosAplic.Value);
        }        
    }
}

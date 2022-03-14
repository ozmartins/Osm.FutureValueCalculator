using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Contrib.HttpClient;
using Newtonsoft.Json;
using Osm.FutureValueCalculator.App.Apps;
using Osm.FutureValueCalculator.App.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.Test.App
{
    [TestClass]
    public class InterestRateAppTest
    {       
        //The main goal of this class is to know if the classe returns the values returned by the remote service.
        //So, I won't execute a test for each possible situation, because it was done by FutureValueServiceTest.

        [TestMethod]
        public async Task InterestRateAppTest_GettingOkResult()
        {
            #region arrange           
            var expectedInterestRate = 0.1f;
            var fakeUrl = "http://www.test.com";                                   
            var fakeInteresRateModel = new InterestRateModel() { Value = expectedInterestRate };
                       
            var handler = new Mock<HttpMessageHandler>();
            var fakeHttpClient = handler.CreateClient();
            
            handler
                .SetupRequest(HttpMethod.Get, fakeUrl)
                .ReturnsResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(fakeInteresRateModel), "application/json");

            var interestRateApp = new InterestRateApp(fakeHttpClient);
            #endregion

            #region act
            var getInterestRateResult = await interestRateApp.GetInterestRateAsync(fakeUrl);
            #endregion

            #region assert
            Assert.IsNotNull(getInterestRateResult);
            Assert.IsTrue(getInterestRateResult.Success);            
            Assert.IsNotNull(getInterestRateResult.InterestRateModel);            
            Assert.AreEqual(getInterestRateResult.InterestRateModel.Value, expectedInterestRate);
            #endregion
        }

        [TestMethod]
        public async Task InterestRateAppTest_GettingInternalServerError()
        {
            #region arrange                       
            var fakeUrl = "http://www.test.com";
            var fakeInteresRateModel = new InterestRateModel() { Value = 0 };

            var handler = new Mock<HttpMessageHandler>();
            var fakeHttpClient = handler.CreateClient();

            handler
                .SetupRequest(HttpMethod.Get, fakeUrl)
                .ReturnsResponse(HttpStatusCode.InternalServerError, "", "application/json");

            var interestRateApp = new InterestRateApp(fakeHttpClient);
            #endregion

            #region act
            var getInterestRateResult = await interestRateApp.GetInterestRateAsync(fakeUrl);
            #endregion

            #region assert
            Assert.IsNotNull(getInterestRateResult);
            Assert.IsFalse(getInterestRateResult.Success);
            Assert.IsNotNull(getInterestRateResult.InterestRateModel);
            Assert.AreEqual(getInterestRateResult.InterestRateModel.Value, 0);
            Assert.AreEqual(getInterestRateResult.Errors.Count, 1);
            Assert.AreEqual(getInterestRateResult.Errors[0], "Internal Server Error");
            #endregion
        }
    }
}

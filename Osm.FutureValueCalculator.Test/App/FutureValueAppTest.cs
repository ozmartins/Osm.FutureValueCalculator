using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Osm.FutureValueCalculator.App.Apps;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.App.Models;
using Osm.FutureValueCalculator.Domain.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.Test.App
{
    [TestClass]
    public class FutureValueAppTest
    {       
        [TestMethod]
        public async Task FutureValueAppTest_CalculatingFutureValueWithValidParameters()
        {
            #region arrange
            var fakeUrl = "http://www.teste.com";

            var expectedFutureValueCalcResult = new FutureValueCalcResult()
            { 
                Success = true,
                FutureValue = 123.45m
            };

            var expectedInterestRateResult = new GetInterestRateResult()
            {
                Success = true,
                InterestRateModel = new InterestRateModel()
            };

            var futureValueServiceMock = new Mock<IFutureValueService>();
            futureValueServiceMock
                .Setup(x => x.CalculateFutureValue(1.0m, 2.0f, 3))
                .Returns(expectedFutureValueCalcResult);

            var interestRateAppMock = new Mock<IInterestRateApp>();
            interestRateAppMock
                .Setup(x => x.GetInterestRateAsync(fakeUrl).Result)
                .Returns(expectedInterestRateResult);

            var futureValueApp = new FutureValueApp(futureValueServiceMock.Object, interestRateAppMock.Object);
            #endregion

            #region act
            var futureValueCalcResult = await futureValueApp.CalculateFutureValue(1.0m, 3);
            #endregion

            #region assert
            Assert.IsNotNull(futureValueCalcResult);
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValueCalcResult.FutureValue);
            #endregion
        }

        [TestMethod]
        public async Task FutureValueAppTest_CalculatingFutureValueWithNullInterestRateResult()
        {
            #region arrange
            var fakeUrl = "http://www.teste.com";

            var expectedErrorMessage = "The interest rate API didn't return any data. Please contact tech support.";

            var expectedFutureValueCalcResult = new FutureValueCalcResult()
            {
                Success = false,
                Errors = new List<string>() { expectedErrorMessage }
            };            

            var futureValueServiceMock = new Mock<IFutureValueService>();
            futureValueServiceMock
                .Setup(x => x.CalculateFutureValue(1.0m, 2.0f, 3))
                .Returns(expectedFutureValueCalcResult);

            var interestRateAppMock = new Mock<IInterestRateApp>();
            interestRateAppMock
                .Setup(x => x.GetInterestRateAsync(fakeUrl).Result);

            var futureValueApp = new FutureValueApp(futureValueServiceMock.Object, interestRateAppMock.Object);
            #endregion

            #region act
            var futureValueCalcResult = await futureValueApp.CalculateFutureValue(1.0m, 3);
            #endregion

            #region assert
            Assert.IsNotNull(futureValueCalcResult);
            Assert.IsFalse(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 1);
            Assert.AreEqual(futureValueCalcResult.Errors[0], expectedErrorMessage);
            #endregion
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Osm.FutureValueCalculator.App.Apps;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;

namespace Osm.FutureValueCalculator.Test.App
{
    [TestClass]
    public class FutureValueAppTest
    {
        ///The main goal of this test is just to know if FutureValueApp return FutureValueCalcResult created by the service.
        ///Because off that, I won't create a test for each scenario tested for the FutureValueService class.
        
        [TestMethod]        
        public async void FutureValueAppTest_CalculatingFutureValueWithValidParameters()
        {                        
            #region arrange
            var expectedFutureValue = 123.456m;
            var expectedFutureValueCalcResult = new FutureValueCalcResult() { Success = true, FutureValue = expectedFutureValue };

            var futureValueServiceMock = new Mock<IFutureValueService>();
            futureValueServiceMock.Setup(p => p.CalculateFutureValue(1, 2, 3)).Returns(expectedFutureValueCalcResult);

            var interestRateAppMock = new Mock<IInterestRateApp>();
            interestRateAppMock.Setup(p => p.GetInterestRate());//.Returns(new InterestRateModel() { Value = 2 });

            var futureValueApp = new FutureValueApp(futureValueServiceMock.Object, interestRateAppMock.Object);
            #endregion

            #region act
            var futureValueServiceResult = await futureValueApp.CalculateFutureValue(1, 3);
            #endregion

            #region assert
            Assert.IsNotNull(futureValueServiceResult);
            Assert.IsTrue(futureValueServiceResult.Success);
            Assert.AreEqual(futureValueServiceResult.Errors.Count, 0);
            Assert.AreEqual(futureValueServiceResult.FutureValue, expectedFutureValue);
            #endregion
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Osm.FutureValueCalculator.Api.Controllers;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.Test.Api
{
    [TestClass]
    public class FutureValueControllerTest
    {
        [TestMethod]
        public async Task FutureValueControllerTest_GettingFutureValueWithValidParameters()
        {
            #region             
            var presentValue = 100m;
            var months = 10;            
            var expectedFutureValue = 1000m;

            var expectedFutureValueCalcResult = new FutureValueCalcResult()
            {
                Success = true,
                FutureValue = expectedFutureValue
            };

            var futureValueAppMock = new Mock<IFutureValueApp>();
            futureValueAppMock
                .Setup(x => x.CalculateFutureValueAsync(presentValue, months))
                .ReturnsAsync(expectedFutureValueCalcResult);

            var futureValueController = new FutureValueController(futureValueAppMock.Object);
            #endregion

            #region act
            var actionResult = await futureValueController.Get(presentValue, months);
            #endregion

            #region assert            
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            Assert.IsNotNull(((OkObjectResult)actionResult.Result).Value);
            Assert.IsInstanceOfType(((OkObjectResult)actionResult.Result).Value, typeof(FutureValueCalcResult));

            Assert.AreEqual(_getFutureValueCalcResultFrom(actionResult.Result).FutureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public async Task FutureValueControllerTest_GettingFutureValueWithNullFutureValueCalcResult()
        {
            #region             
            var presentValue = 100m;
            var months = 10;            
            var expectedErrorMessage = "Something went wrong. The system couldn't find the interest rate. Please, contact tech support or try again later.";            

            var futureValueAppMock = new Mock<IFutureValueApp>();
            futureValueAppMock
                .Setup(x => x.CalculateFutureValueAsync(presentValue, months));                

            var futureValueController = new FutureValueController(futureValueAppMock.Object);
            #endregion

            #region act
            var actionResult = await futureValueController.Get(presentValue, months);
            #endregion

            #region assert            
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)actionResult.Result).StatusCode, 500);
            Assert.IsNotNull(((ObjectResult)actionResult.Result).Value);
            Assert.IsInstanceOfType(((ObjectResult)actionResult.Result).Value, typeof(FutureValueCalcResult));
            Assert.AreEqual(((FutureValueCalcResult)((ObjectResult)actionResult.Result).Value).Errors.Count, 1);
            Assert.AreEqual(((FutureValueCalcResult)((ObjectResult)actionResult.Result).Value).Errors[0], expectedErrorMessage);
            #endregion
        }

        [TestMethod]
        public async Task FutureValueControllerTest_ExceptionWhenGettingFutureValue()
        {
            #region             
            var presentValue = 100m;
            var months = 10;
            var expectedErrorMessage = "Test";

            var futureValueAppMock = new Mock<IFutureValueApp>();
            futureValueAppMock
                .Setup(x => x.CalculateFutureValueAsync(presentValue, months))
                .Throws(new Exception(expectedErrorMessage));

            var futureValueController = new FutureValueController(futureValueAppMock.Object);
            #endregion

            #region act
            var actionResult = await futureValueController.Get(presentValue, months);
            #endregion

            #region assert            
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)actionResult.Result).StatusCode, 500);
            Assert.IsNotNull(((ObjectResult)actionResult.Result).Value);
            Assert.IsInstanceOfType(((ObjectResult)actionResult.Result).Value, typeof(FutureValueCalcResult));
            Assert.AreEqual(((FutureValueCalcResult)((ObjectResult)actionResult.Result).Value).Errors.Count, 1);
            Assert.AreEqual(((FutureValueCalcResult)((ObjectResult)actionResult.Result).Value).Errors[0], expectedErrorMessage);
            #endregion
        }

        private FutureValueCalcResult _getFutureValueCalcResultFrom(ActionResult actionResult)
        {
            var okObjectResult = ((OkObjectResult)actionResult);

            return (FutureValueCalcResult)okObjectResult.Value;
        }
    }
}

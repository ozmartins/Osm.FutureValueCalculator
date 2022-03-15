﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Osm.FutureValueCalculator.Api.Controllers;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
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
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));

            Assert.IsNotNull(((OkObjectResult)actionResult).Value);
            Assert.IsInstanceOfType(((OkObjectResult)actionResult).Value, typeof(FutureValueCalcResult));

            Assert.AreEqual(_getFutureValueCalcResultFrom(actionResult).FutureValue, expectedFutureValue);
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
            Assert.IsInstanceOfType(actionResult, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)actionResult).StatusCode, 500);

            Assert.IsNotNull(((ObjectResult)actionResult).Value);
            Assert.IsTrue(((ObjectResult)actionResult).Value is string);

            Assert.AreEqual(((ObjectResult)actionResult).Value, expectedErrorMessage);
            #endregion
        }

        private FutureValueCalcResult _getFutureValueCalcResultFrom(ActionResult actionResult)
        {
            var okObjectResult = ((OkObjectResult)actionResult);

            return (FutureValueCalcResult)okObjectResult.Value;
        }
    }
}
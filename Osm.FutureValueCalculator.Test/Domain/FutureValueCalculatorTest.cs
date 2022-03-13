using Microsoft.VisualStudio.TestTools.UnitTesting;
using Osm.FutureValueCalculator.Domain.Services;
using System;

namespace Osm.FutureValueCalculator.Test.Domain
{
    [TestClass]
    public class FutureValueCalculatorTest
    {
        [TestMethod]
        public void CalculatingFutureValueWithValidParameters()
        {
            #region arrange            
            var presentValue = 100m;
            var monthlyInterestRate = 0.01;
            var months = 5;
            var expectedFutureValue = 105.1m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }
        
        [TestMethod]
        public void CalculatingFutureValueWithNegativeParameters()
        {
            #region arrange            
            var presentValue = -100m;
            var monthlyInterestRate = -0.01;
            var months = -5;
            var expectedFutureValue = -105.15m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void CalculatingFutureValueWithFloatingPresentValue()
        {
            #region arrange            
            var presentValue = 123.456m;
            var monthlyInterestRate = 0.01;
            var months = 5;
            var expectedFutureValue = 129.75m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void CalculatingFutureValueWithBigNumber()
        {
            #region arrange            
            var presentValue = decimal.MaxValue;
            var monthlyInterestRate = double.MaxValue;
            var months = int.MaxValue;
            var expectedErrorMessage = "System.OverflowException: Value was either too large or too small for a Decimal.";
            var futureValueService = new FutureValueService();
            #endregion

            #region act + assert                             
            Assert.ThrowsException<OverflowException>(() => futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months), expectedErrorMessage);
            #endregion            
        }

        [TestMethod]
        public void CalculatingFutureValueWithZeroedParameters()
        {
            #region arrange            
            var presentValue = 0m;
            var monthlyInterestRate = 0.0;
            var months = 0;
            var expectedFutureValue = 0m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void CalculatingFutureValueWithZeroedPresentValue()
        {
            #region arrange            
            var presentValue = 0m;
            var monthlyInterestRate = 0.01;
            var months = 5;
            var expectedFutureValue = 0m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void CalculatingFutureValueWithZeroedInterestRate()
        {
            #region arrange            
            var presentValue = 100m;
            var monthlyInterestRate = 0.00;
            var months = 5;
            var expectedFutureValue = 100m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void CalculatingFutureValueWithZeroedTime()
        {
            #region arrange            
            var presentValue = 100m;
            var monthlyInterestRate = 0.01;
            var months = 0;
            var expectedFutureValue = 100m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValue = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.AreEqual(futureValue, expectedFutureValue);
            #endregion
        }        
    }
}

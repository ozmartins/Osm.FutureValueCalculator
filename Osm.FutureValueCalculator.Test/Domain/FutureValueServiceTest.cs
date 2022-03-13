using Microsoft.VisualStudio.TestTools.UnitTesting;
using Osm.FutureValueCalculator.Domain.Services;

namespace Osm.FutureValueCalculator.Test.Domain
{
    [TestClass]
    public class FutureValueServiceTest
    {
        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithValidParametersUsing()
        {
            #region arrange            
            var presentValue = 100m;
            var monthlyInterestRate = 0.01;
            var months = 5;
            var expectedFutureValue = 105.1m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValue);
            #endregion
        }
        
        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithNegativeParameters()
        {
            #region arrange            
            var presentValue = -100m;
            var monthlyInterestRate = -0.01;
            var months = -5;           
            var futureValueService = new FutureValueService();
            var expectedErrorMessage1 = "The present value can't be lower than zero.";
            var expectedErrorMessage2 = "The number of months can't be lower than zero.";
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsFalse(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 2);
            Assert.AreEqual(futureValueCalcResult.Errors[0], expectedErrorMessage1);
            Assert.AreEqual(futureValueCalcResult.Errors[1], expectedErrorMessage2);
            Assert.AreEqual(futureValueCalcResult.FutureValue, 0);
            #endregion
        }

        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithFloatingPresentValue()
        {
            #region arrange            
            var presentValue = 123.456m;
            var monthlyInterestRate = 0.01;
            var months = 5;
            var expectedFutureValue = 129.75m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithBigNumber()
        {
            #region arrange            
            var presentValue = decimal.MaxValue;
            var monthlyInterestRate = double.MaxValue;
            var months = int.MaxValue;
            var expectedErrorMessage = "Value was either too large or too small for a Decimal.";
            var futureValueService = new FutureValueService();
            #endregion

            #region act
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region assert
            Assert.IsFalse(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 1);
            Assert.AreEqual(futureValueCalcResult.Errors[0], expectedErrorMessage);            
            Assert.AreEqual(futureValueCalcResult.FutureValue, 0);
            #endregion
        }

        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithZeroedParameters()
        {
            #region arrange            
            var presentValue = 0m;
            var monthlyInterestRate = 0.0;
            var months = 0;
            var expectedFutureValue = 0m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithZeroedPresentValue()
        {
            #region arrange            
            var presentValue = 0m;
            var monthlyInterestRate = 0.01;
            var months = 5;
            var expectedFutureValue = 0m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithZeroedInterestRate()
        {
            #region arrange            
            var presentValue = 100m;
            var monthlyInterestRate = 0.00;
            var months = 5;
            var expectedFutureValue = 100m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValue);
            #endregion
        }

        [TestMethod]
        public void FutureValueServiceTest_CalculatingFutureValueWithZeroedTime()
        {
            #region arrange            
            var presentValue = 100m;
            var monthlyInterestRate = 0.01;
            var months = 0;
            var expectedFutureValue = 100m;
            var futureValueService = new FutureValueService();
            #endregion

            #region act     
            var futureValueCalcResult = futureValueService.CalculateFutureValue(presentValue, monthlyInterestRate, months);
            #endregion

            #region asssert  
            Assert.IsTrue(futureValueCalcResult.Success);
            Assert.AreEqual(futureValueCalcResult.Errors.Count, 0);
            Assert.AreEqual(futureValueCalcResult.FutureValue, expectedFutureValue);
            #endregion
        }        
    }
}

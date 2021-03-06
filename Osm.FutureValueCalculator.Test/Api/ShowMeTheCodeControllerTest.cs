using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Osm.FutureValueCalculator.Api.Controllers;
using Osm.FutureValueCalculator.App.Models;

namespace Osm.FutureValueCalculator.Test.Api
{
    [TestClass]
    public class ShowMeTheCodeControllerTest
    {
        [TestMethod]
        public void ShowMeTheCodeControllerTest_GettingValidData()
        {
            #region arrange
            var expectedGitHubDescription = "GitHub";
            var expectedGitHubInterestRateUrl = "interest-rate.github";
            var expectedGitHubFutureValueCalculatorUrl = "future-value-calculator.github";

            var expectedHerokuDescription = "Heroku";
            var expectedHerokuInterestRateUrl = "interest-rate.heroku";
            var expectedHerokuFutureValueCalculatorUrl = "future-value-calculator.heroku";
            
            var expectedshowMeTheCodeModel = new ShowMeTheCodeModel()
            {
                GitHub = new GitHubModel() 
                { 
                    Description = expectedGitHubDescription,
                    InterestRateUrl = expectedGitHubInterestRateUrl,
                    FutureValueCalculatorUrl = expectedGitHubFutureValueCalculatorUrl
                },

                Heroku = new HerokuModel()
                {
                    Description = expectedHerokuDescription,
                    InterestRateUrl = expectedHerokuInterestRateUrl,
                    FutureValueCalculatorUrl = expectedHerokuFutureValueCalculatorUrl
                },
            };            

            IOptions<ShowMeTheCodeModel> showMeTheCodeModelOptions = Options.Create(expectedshowMeTheCodeModel);
           
            var showMeTheCodeController = new ShowMeTheCodeController(showMeTheCodeModelOptions);
            #endregion

            #region act
            var actionResult = showMeTheCodeController.Get();
            #endregion

            #region assert            
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));

            Assert.IsNotNull(((OkObjectResult)actionResult.Result).Value);
            Assert.IsInstanceOfType(((OkObjectResult)actionResult.Result).Value, typeof(ShowMeTheCodeModel));

            Assert.AreEqual(_getShowMeTheCodeModelFrom(actionResult.Result).GitHub.Description, expectedGitHubDescription);
            Assert.AreEqual(_getShowMeTheCodeModelFrom(actionResult.Result).GitHub.InterestRateUrl, expectedGitHubInterestRateUrl);
            Assert.AreEqual(_getShowMeTheCodeModelFrom(actionResult.Result).GitHub.FutureValueCalculatorUrl, expectedGitHubFutureValueCalculatorUrl);

            Assert.AreEqual(_getShowMeTheCodeModelFrom(actionResult.Result).Heroku.Description, expectedHerokuDescription);
            Assert.AreEqual(_getShowMeTheCodeModelFrom(actionResult.Result).Heroku.InterestRateUrl, expectedHerokuInterestRateUrl);
            Assert.AreEqual(_getShowMeTheCodeModelFrom(actionResult.Result).Heroku.FutureValueCalculatorUrl, expectedHerokuFutureValueCalculatorUrl);
            #endregion
        }

        private ShowMeTheCodeModel _getShowMeTheCodeModelFrom(ActionResult actionResult)
        {
            var okObjectResult = ((OkObjectResult)actionResult);

            return (ShowMeTheCodeModel)okObjectResult.Value;
        }
    }
}

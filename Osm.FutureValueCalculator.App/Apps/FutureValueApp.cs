using Osm.FutureValueCalculator.App.Infra;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Apps
{
    public class FutureValueApp : IFutureValueApp
    {
        private readonly IFutureValueService _futureValueService;

        private readonly IInterestRateApp _interestRateApp;        

        public FutureValueApp(IFutureValueService futureValueService, IInterestRateApp interestRateApp)
        {
            _futureValueService = futureValueService;
            _interestRateApp = interestRateApp;
        }

        public async Task<FutureValueCalcResult> CalculateFutureValueAsync(decimal presentValue, int months)
        {           
            var interestRateResult = await _interestRateApp.GetInterestRateAsync(ConfigVars.InterestRateApiUrl());

            if (interestRateResult == null)
            {
                return new FutureValueCalcResult()
                {
                    Success = false,
                    Errors = new List<string>() { "The interest rate API didn't return any data. Please contact tech support." }
                };
            }

            if (interestRateResult.InterestRateModel == null)
            {
                return new FutureValueCalcResult()
                {
                    Success = false,
                    Errors = new List<string>() { "The interest rate API returned null interest rate data. Please contact tech support." }
                };
            }

            var FutureValueCalcResult = _futureValueService.CalculateFutureValue(presentValue, interestRateResult.InterestRateModel.Value, months);

            return FutureValueCalcResult;
        }
    }
}

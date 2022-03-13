using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;

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

        public FutureValueCalcResult CalculateFutureValue(decimal presentValue, int months)
        {
            var interestRateModel = _interestRateApp.GetInterestRate();

            return _futureValueService.CalculateFutureValue(presentValue, interestRateModel.Value, months);
        }
    }
}

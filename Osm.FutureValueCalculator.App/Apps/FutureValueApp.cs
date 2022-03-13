using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Apps
{
    public class FutureValueApp
    {
        private readonly IFutureValueService _futureValueService;

        private readonly IInterestRateApp _interestRateApp;

        public FutureValueApp(IFutureValueService futureValueService, IInterestRateApp interestRateApp)
        {
            _futureValueService = futureValueService;
            _interestRateApp = interestRateApp;
        }

        public async Task<FutureValueCalcResult> CalculateFutureValue(decimal presentValue, int months)
        {
            var interestRateModel = await _interestRateApp.GetInterestRate();

            return _futureValueService.CalculateFutureValue(presentValue, interestRateModel.Value, months);
        }
    }
}

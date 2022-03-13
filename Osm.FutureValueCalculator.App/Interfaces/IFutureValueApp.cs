using Osm.FutureValueCalculator.Domain.Models;

namespace Osm.FutureValueCalculator.App.Interfaces
{
    public interface IFutureValueApp
    {
        public FutureValueCalcResult CalculateFutureValue(decimal presentValue, int months);
    }
}

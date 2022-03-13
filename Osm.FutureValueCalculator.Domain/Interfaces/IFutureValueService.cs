using Osm.FutureValueCalculator.Domain.Models;

namespace Osm.FutureValueCalculator.Domain.Interfaces
{
    public interface IFutureValueService
    {
        FutureValueCalcResult CalculateFutureValue(decimal presentValue, double monthlyInterestRate, int months);
    }
}

using Osm.FutureValueCalculator.Domain.Models;

namespace Osm.FutureValueCalculator.App.Interfaces
{
    public interface IInterestRateApp
    {
        InterestRateModel GetInterestRate();
    }
}

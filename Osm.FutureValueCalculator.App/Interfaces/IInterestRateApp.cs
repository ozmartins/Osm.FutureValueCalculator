using Osm.FutureValueCalculator.App.Models;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Interfaces
{
    public interface IInterestRateApp
    {
        Task<InterestRateResultModel> GetInterestRateAsync(string interestRateApiUrl);
    }
}

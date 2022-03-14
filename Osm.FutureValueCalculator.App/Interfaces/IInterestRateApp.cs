using Osm.FutureValueCalculator.Domain.Models;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Interfaces
{
    public interface IInterestRateApp
    {
        Task<InterestRateModel> GetInterestRateAsync(string interestRateApiUrl);
    }
}

using Osm.FutureValueCalculator.Domain.Models;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.App.Interfaces
{
    public interface IFutureValueApp
    {
        Task<FutureValueCalcResult> CalculateFutureValue(decimal presentValue, int months);
    }
}

using System.Collections.Generic;

namespace Osm.FutureValueCalculator.App.Models
{
    public class GetInterestRateResult
    {
        public bool Success { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
        public InterestRateModel InterestRateModel { get; set; } = new InterestRateModel() { Value = 0 };
    }
}

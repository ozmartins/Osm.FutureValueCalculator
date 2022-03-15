using System.Diagnostics.CodeAnalysis;

namespace Osm.FutureValueCalculator.App.Models
{
    [ExcludeFromCodeCoverage]
    public class ProjectDataModel
    {
        public string Description{ get; set; }
        public string InterestRateUrl { get; set; }
        public string FutureValueCalculatorUrl { get; set; }       
    }
}

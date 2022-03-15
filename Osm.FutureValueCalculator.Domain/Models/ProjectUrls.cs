using System.Diagnostics.CodeAnalysis;

namespace Osm.FutureValueCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class ProjectUrls
    {
        public string Description{ get; set; }
        public string InterestRate { get; set; }
        public string FutureValueCalculator { get; set; }       
    }
}

using System.Collections.Generic;

namespace Osm.FutureValueCalculator.Domain.Models
{
    public class FutureValueServiceResult
    {
        public bool Success { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
        public decimal FutureValue { get; set; } = 0;
    }
}

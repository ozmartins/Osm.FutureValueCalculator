using System;

namespace Osm.FutureValueCalculator.App.Extensions
{
    public static class ConfigVars
    {
        public static string InterestRateApiUrl()
        {
            return Environment.GetEnvironmentVariable("INTEREST_RATE_API");
        }
    }
}

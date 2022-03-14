using System;

namespace Osm.FutureValueCalculator.App.Infra
{
    public static class ConfigVars
    {
        public static string InterestRateApiUrl()
        {
            return Environment.GetEnvironmentVariable("INTEREST_RATE_API");
        }
    }
}

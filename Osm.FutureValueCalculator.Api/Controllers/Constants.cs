namespace Osm.FutureValueCalculator.Api.Controllers
{
    public class Constants
    {
        public const string ShowMeTheCodeGetSummary = "Returns a JSON with the project's source code URL and the API's production URL.";
        public const string ShowMeTheCodeGetDescription = "The return of this endpoint has two main sessions. One shows two URLs for two different GitHub repositories e the other section shows the two URLs for two different running apps.";
        public const string ShowMeTheCodeTag = "Show me the code";
        public const string FutureValueGetSummary = "Calculates the future value of 'present value' after 'X' months.";
        public const string FutureValueGetDescription = "Returns the result of the following expression: "+
                                                        "(PV * (1 + i) ^ n)." +
                                                        "Where: " +
                                                        "PV = Present value, " +
                                                        "i = interest rate returned by InterestRateApi, " +
                                                        "^ = Stands for power operation and " +
                                                        "n = number of months";
        public const string FutureValueTag = "Future value";
        public const string NullInterestRateMessage = "Something went wrong. The system couldn't find the interest rate. Please, contact tech support or try again later.";
    }
}

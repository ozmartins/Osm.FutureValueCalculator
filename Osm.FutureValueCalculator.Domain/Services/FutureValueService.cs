using Osm.FutureValueCalculator.Domain.Models;
using System;
using System.Collections.Generic;

namespace Osm.FutureValueCalculator.Domain.Services
{
    public class FutureValueService
    {
        public FutureValueServiceResult CalculateFutureValue(decimal presentValue, double monthlyInterestRate, int months)
        {
            try
            {
                var result = _validateParameters(presentValue, months);

                if (!result.Success) return result;

                var multiplier = (decimal)Math.Pow(1 + monthlyInterestRate, months);

                var futureValue = presentValue * multiplier;

                var truncatedFutureValue = Math.Truncate(futureValue * 100) / 100;

                return new FutureValueServiceResult()
                {
                    Success = true,
                    FutureValue = truncatedFutureValue
                };
            }            
            catch (Exception e)
            {
                return new FutureValueServiceResult()
                {
                    Success = false,
                    Errors = new List<string>() { e.Message }
                };
            }            
        }

        private FutureValueServiceResult _validateParameters(decimal presentValue, int months)
        {
            //Note that I didn't validate then negative interest rate.
            //I have made that decision because some countries work with negative interest rates.
            //It can be strange for us, Brazilians, but is a reality in some rich countries.

            var errors = new List<string>();

            if (presentValue < 0)
            {
                errors.Add("The present value can't be lower than zero.");
            }

            if (months < 0)
            {
                errors.Add("The number of months can't be lower than zero.");
            }

            return new FutureValueServiceResult()
            {
                Success = errors.Count == 0,
                Errors = errors
            };
        }
    }
}

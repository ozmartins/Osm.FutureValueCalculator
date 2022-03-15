using Microsoft.AspNetCore.Mvc;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Osm.FutureValueCalculator.Api.Controllers
{
    [ApiController]
    [Route("CalculaJuros")]
    public class FutureValueController : ControllerBase
    {
        private readonly IFutureValueApp _futureValueApp;

        public FutureValueController(IFutureValueApp futureValueApp)
        {
            _futureValueApp = futureValueApp;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = Constants.FutureValueGetSummary, Description = Constants.FutureValueGetDescription, Tags = new[] { Constants.FutureValueTag })]
        public async Task<ActionResult<FutureValueCalcResult>> Get([FromQuery(Name = "ValorInicial")] decimal presentValue, [FromQuery(Name = "Meses")] int months)
        {
            try
            {
                var futureValueCalcResult = await _futureValueApp.CalculateFutureValueAsync(presentValue, months);

                if (futureValueCalcResult == null)
                {
                    return StatusCode(500, Constants.NullInterestRateMessage);
                }

                return Ok(futureValueCalcResult);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }        
    }
}

using Microsoft.AspNetCore.Mvc;
using Osm.FutureValueCalculator.App.Interfaces;
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
        public async Task<ActionResult> Get([FromQuery(Name = "ValorInicial")] decimal presentValue, [FromQuery(Name = "Meses")] int months)
        {
            try
            {
                var futureValueCalcResult = await _futureValueApp.CalculateFutureValue(presentValue, months);

                if (futureValueCalcResult == null)
                {
                    return StatusCode(500, "Something went wrong. The system couldn't find the interest rate. Please, contact tech support or try again later.");
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

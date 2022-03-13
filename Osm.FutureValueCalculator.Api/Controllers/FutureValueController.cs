using Microsoft.AspNetCore.Mvc;
using Osm.FutureValueCalculator.App.Interfaces;
using System;

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
        public ActionResult Get([FromRoute(Name= "ValorInicial")] decimal presentValue, [FromRoute(Name = "Meses")] int months)
        {
            try
            {
                var futureValueCalcResult = _futureValueApp.CalculateFutureValue(presentValue, months);

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

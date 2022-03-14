using Microsoft.AspNetCore.Mvc;

namespace Osm.FutureValueCalculator.Api.Controllers
{
    [ApiController]
    [Route("ShowMeTheCode")]
    public class ShowMeTheCodeController : ControllerBase
    {               
        [HttpGet]
        [ProducesResponseType(200)]        
        public ActionResult Get()
        {
            return Ok(new 
            { 
                GitUrlForInterestRate = "https://github.com/ozmartins/Osm.InterestRate",
                GitUrlForFutureValueCalculator = "https://github.com/ozmartins/Osm.FutureValueCalculator",
                HerokuUrlForInterestRate = "https://osm-interest-rate.herokuapp.com",
                HerokuUrlForFutureValueCalculator = "https://osm-future-value-calculator.herokuapp.com",
            });

        }
    }
}

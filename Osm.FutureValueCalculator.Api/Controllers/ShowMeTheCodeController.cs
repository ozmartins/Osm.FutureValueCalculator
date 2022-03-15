using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Osm.FutureValueCalculator.App.Models;

namespace Osm.FutureValueCalculator.Api.Controllers
{
    [ApiController]
    [Route("ShowMeTheCode")]
    public class ShowMeTheCodeController : ControllerBase
    {
        private IOptions<ShowMeTheCodeModel> _showMeTheCodeModelOptions;

        public ShowMeTheCodeController(IOptions<ShowMeTheCodeModel> showMeTheCodeModelOptions)
        {
            _showMeTheCodeModelOptions = showMeTheCodeModelOptions;
        }

        [HttpGet]
        [ProducesResponseType(200)]        
        public ActionResult Get()
        {
            return Ok(_showMeTheCodeModelOptions.Value);
        }
    }
}

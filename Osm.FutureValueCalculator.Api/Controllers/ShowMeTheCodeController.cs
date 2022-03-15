using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Osm.FutureValueCalculator.App.Models;

namespace Osm.FutureValueCalculator.Api.Controllers
{
    [ApiController]
    [Route("ShowMeTheCode")]
    public class ShowMeTheCodeController : ControllerBase
    {
        private IOptions<ShowMeTheCodeModel> _showMeTheCodeModel;

        public ShowMeTheCodeController(IOptions<ShowMeTheCodeModel> showMeTheCodeModel)
        {
            _showMeTheCodeModel = showMeTheCodeModel;
        }

        [HttpGet]
        [ProducesResponseType(200)]        
        public ActionResult Get()
        {
            return Ok(_showMeTheCodeModel);
        }
    }
}

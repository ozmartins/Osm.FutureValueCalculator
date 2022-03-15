using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Osm.FutureValueCalculator.App.Models;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = Constants.ShowMeTheCodeGetSummary, Description = Constants.ShowMeTheCodeGetDescription, Tags = new[] { Constants.ShowMeTheCodeTag })]
        public ActionResult<ShowMeTheCodeModel> Get()
        {
            return Ok(_showMeTheCodeModelOptions.Value);
        }
    }
}

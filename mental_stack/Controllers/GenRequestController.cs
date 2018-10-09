using MentalStack.Entities;
using MentalStack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MentalStack.Controllers
{
    [Route("")]
    public class GenRequestController : Controller
    {
        private readonly MStackService _mStackService;
        private readonly ILogger<GenRequestController> _logger;

        public GenRequestController(MStackService mStackService, ILogger<GenRequestController> logger)
        {
            _mStackService = mStackService;
            _logger = logger;
            _logger.LogInformation("GenRequestController started");
        }

        [HttpPost]
        public IActionResult GetRequest([FromBody] GenRequest genRequest)
        {
            return Ok(genRequest.Process(_mStackService));
        }
    }
}

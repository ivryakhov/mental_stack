using MentalStack.Entities;
using MentalStack.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentalStack.Controllers
{
    [Route("")]
    public class GenRequestController : Controller
    {
        private MStackService _mStackService;
        public GenRequestController(MStackService mStackService)
        {
            _mStackService = mStackService;
        }

        [HttpPost]
        public IActionResult GetRequest([FromBody] GenRequest genRequest)
        {
            return Ok(genRequest.Process(_mStackService));
        }
    }
}

using mental_stack.Entities;
using Microsoft.AspNetCore.Mvc;

namespace mental_stack.Controllers
{
    [Route("")]
    public class GenRequestController : Controller
    {


        [HttpPost]
        public IActionResult GetRequest([FromBody] GenRequest genRequest)
        {
            return Ok(genRequest);
        }
    }
}

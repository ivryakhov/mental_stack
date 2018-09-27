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
            string reply = "";
           /* switch (genRequest.Request.Kind)
            {
                case Entities.Request.RequestKind.Push:
                    var res = _mStackService.Push(genRequest.Session.UserId,
                                                      genRequest.Request.OriginalUtterance);
                    break;
                case Entities.Request.RequestKind.Pop:
                    var res2 = _mStackService.Pop(genRequest.Session.UserId, out reply);
                    break;
                default:
                    reply = "BADCOMMAND";
                    break;
            }*/
            return Ok(new GenResponse() { Response = new Response() { Text = reply } });
        }
    }
}

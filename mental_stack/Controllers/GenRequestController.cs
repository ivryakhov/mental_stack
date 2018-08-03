using mental_stack.Entities;
using mental_stack.Services;
using Microsoft.AspNetCore.Mvc;

namespace mental_stack.Controllers
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
            switch (genRequest.Request.Kind)
            {
                case Entities.Request.RequestKind.Push:
                    reply = _mStackService.Push(genRequest.Session.UserId,
                                                      genRequest.Request.OriginalUtterance);
                    break;
                case Entities.Request.RequestKind.Pop:
                    reply = _mStackService.Pop(genRequest.Session.UserId);
                    break;
                default:
                    reply = "BADCOMMAND";
                    break;
            }
            return Ok(new GenResponse() { Response = new Response() { Text = reply } });
        }
    }
}

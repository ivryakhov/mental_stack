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
        public IActionResult GetRequestAsync([FromBody] GenRequest genRequest)
        {
            var command = genRequest.Request.Parse();
            string reply;
            switch (command)
            {
                case "PUSH":
                    reply = _mStackService.Push(genRequest.Session.UserId,
                                                      genRequest.Request.OriginalUtterance);
                    break;
                case "POP":
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

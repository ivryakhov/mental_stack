using MentalStack.Services;

namespace MentalStack.Entities
{
    public class GenRequest
    {
        public Meta Meta { get; set; }
        public Request Request { get; set; }
        public Session Session { get; set; }
        public string Version { get; set; }

        public GenResponse Process(MStackService mStackService)
        {
            var workRequest = Request.CreateWorkRequest(Session.UserId);
            var responceText = workRequest.ProcessRequest(mStackService);

            return new GenResponse()
            {
                Response = new Response()
                {
                    Text = responceText,
                    Tts = responceText,
                    EndSession = true
                },
                Session = Session,
                Version = Version
            };
        }
    }
}

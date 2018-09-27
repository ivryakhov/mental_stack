using MentalStack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentalStack.Entities
{
    public class PopRequest : IWorkRequest
    {
        private string _user;
        private string _command;
        private Markup _markup;
        private string _originalUtterance;
        private Payload _payload;
        private string _type;

        public PopRequest(string user, string command, Markup markup, string originalUtterance,
                           Payload payload, string type)
        {
            _user = user;
            _command = command;
            _markup = markup;
            _originalUtterance = originalUtterance;
            _payload = payload;
            _type = type;
        }

        public string ProcessRequest(MStackService mStackService)
        {
            mStackService.Pop(_user, out string message)
        }
    }
}

using MentalStack.Services;
using System.Collections.Generic;

namespace MentalStack.Entities
{
    public class PopRequest : IWorkRequest
    {
        private readonly Dictionary<MStackService.ResultType, string> _resultMessages =
            new Dictionary<MStackService.ResultType, string>
            {
                { MStackService.ResultType.EmptyStack, "Ваш стэк пуст" },
                { MStackService.ResultType.NoStack, "Чтобы что-то взять, нужно сначала что-то положить" }
            };

        private readonly string _user;

        public PopRequest(string user)
        {
            _user = user;
        }

        public string ProcessRequest(MStackService mStackService)
        {
            var resType = (mStackService.Pop(_user, out string message));
            if (resType == MStackService.ResultType.PopSuccess)
                return message;
            else
                return _resultMessages[resType];
        }
    }
}

using MentalStack.Services;
using System.Collections.Generic;

namespace MentalStack.Entities
{
    public class PushRequest : IWorkRequest
    {
        private readonly Dictionary<MStackService.ResultType, string> _resultMessages =
            new Dictionary<MStackService.ResultType, string>
            {
                { MStackService.ResultType.UserAdded, "Стэк создан и запись успешно добавлена" },
                { MStackService.ResultType.UserUpdated, "Сохранено" }
            };

        private readonly string _user;
        private readonly string _originalUtterance;

        public PushRequest(string user, string originalUtterance)
        {
            _user = user;

            // TODO: parse originalUtterance to clean of service words
            _originalUtterance = originalUtterance;
        }

        public string ProcessRequest(MStackService mStackService)
        {
            var resType = (mStackService.Push(_user, CleanUtterance(_originalUtterance)));
            return _resultMessages[resType];
        }

        private string CleanUtterance (string utterance)
        {
            var parts = utterance.Split("стек");
            return parts[1];
        }
    }
}

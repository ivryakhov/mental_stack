using Newtonsoft.Json;

namespace MentalStack.Entities
{
    public class Request : IWorkRequest
    {
        public enum RequestKind { Push, Pop, Unknown }

        public string Command { get; set; }

        [JsonProperty("original_utterance")]
        public string OriginalUtterance { get; set; }

        public string Type { get; set; }
        public Markup Markup { get; set; }
        public Payload Payload { get; set; }
        public RequestKind Kind { get; }

        public IWorkRequest CreateRequest(string command)
        {
            Command = command;
            var firtsWord = command.Split(' ').GetValue(0).ToString();
            if (string.Equals(firtsWord, "положи", System.StringComparison.OrdinalIgnoreCase))
                return new PushRequest(Command, Markup, OriginalUtterance, Payload, Type);
            else if ((string.Equals(firtsWord, "возьми", System.StringComparison.OrdinalIgnoreCase)))
                return new PopRequest(Command, Markup, OriginalUtterance, Payload, Type);
            else
                return new UnknownRequest();
        }
    }
}

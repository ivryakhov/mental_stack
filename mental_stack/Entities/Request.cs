using Newtonsoft.Json;

namespace MentalStack.Entities
{
    public class Request
    {
        public enum RequestKind { Push, Pop, Unknown }

        public string Command { get; set; }

        [JsonProperty("original_utterance")]
        public string OriginalUtterance { get; set; }

        public string Type { get; set; }
        public Markup Markup { get; set; }
        public Payload Payload { get; set; }
        public RequestKind Kind { get; }

        public Request(string command)
        {
            Command = command;
            var firtsWord = command.Split(' ').GetValue(0).ToString();
            if (string.Equals(firtsWord, "положи", System.StringComparison.OrdinalIgnoreCase))
                Kind = RequestKind.Push;
            else if ((string.Equals(firtsWord, "возьми", System.StringComparison.OrdinalIgnoreCase)))
                Kind = RequestKind.Pop;
            else
                Kind = RequestKind.Unknown;
        }
    }
}

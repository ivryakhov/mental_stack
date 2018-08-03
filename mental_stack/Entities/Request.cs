using Newtonsoft.Json;

namespace mental_stack.Entities
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
            if (Command == "положи на стек")
                Kind = RequestKind.Push;
            else if (Command == "возьми со стека")
                Kind = RequestKind.Pop;
            else
                Kind = RequestKind.Unknown;
        }
    }
}

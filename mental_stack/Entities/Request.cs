using Newtonsoft.Json;

namespace mental_stack.Entities
{
    public class Request
    {
        public string Command { get; set; }

        [JsonProperty("original_utterance")]
        public string OriginalUtterance { get; set; }

        public string Type  { get; set; }
        public Markup Markup { get; set; }
        public Payload Payload { get; set; }
    }
}

using Newtonsoft.Json;

namespace MentalStack.Entities
{
    public class Request
    {
        public string Command { get; set; }

        [JsonProperty("original_utterance")]
        public string OriginalUtterance { get; set; }

        public string Type { get; set; }
        public Markup Markup { get; set; }
        public Payload Payload { get; set; }
        
        public IWorkRequest CreateWorkRequest(string userId, string command)
        {
            Command = command;
            var firtsWord = command.Split(' ').GetValue(0).ToString();
            if (string.Equals(firtsWord, "положи", System.StringComparison.OrdinalIgnoreCase))
                return new PushRequest(userId, OriginalUtterance);
            else if ((string.Equals(firtsWord, "возьми", System.StringComparison.OrdinalIgnoreCase)))
                return new PopRequest(userId);
            else
                return new UnknownRequest();
        }
    }
}

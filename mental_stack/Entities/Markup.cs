using Newtonsoft.Json;

namespace MentalStack.Entities
{
    public class Markup
    {
        [JsonProperty("dangerous_context")]
        public bool DangerousContext { get; set; }
    }
}

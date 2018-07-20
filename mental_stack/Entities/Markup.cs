using Newtonsoft.Json;

namespace mental_stack.Entities
{
    public class Markup
    {
        [JsonProperty("dangerous_context")]
        public bool DangerousContext { get; set; }
    }
}

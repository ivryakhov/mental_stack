using Newtonsoft.Json;

namespace mental_stack.Entities
{
    public class Meta
    {
        public string Locale { get; set; }
        public string Timezone { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}

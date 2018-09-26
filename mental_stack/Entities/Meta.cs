using Newtonsoft.Json;

namespace MentalStack.Entities
{
    public class Meta
    {
        public string Locale { get; set; }
        public string Timezone { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}

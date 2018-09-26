using Newtonsoft.Json;
using System.Collections.Generic;

namespace MentalStack.Entities
{
    public class Response
    {
        public string Text { get; set; }
        public string Tts { get; set; }
        public List<Button> Buttons { get; set; }

        [JsonProperty("end_session")]
        public bool EndSession { get; set; }

    }
}

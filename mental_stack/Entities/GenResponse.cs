using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentalStack.Entities
{
    public class GenResponse
    {
        public Response Response { get; set; }
        public Session Session { get; set; }
        public string Version { get; set; }
    }
}

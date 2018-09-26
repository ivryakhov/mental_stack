using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentalStack.Entities
{
    public class MStack
    {
        public string User { get; set; }
        public Stack<string> Messages { get; set; }
    }
}

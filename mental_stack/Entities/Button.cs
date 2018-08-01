using System;

namespace mental_stack.Entities
{
    public class Button
    {
        public string Title { get; set; }
        public Payload Payload { get; set; }
        public Uri Url { get; set; }
        public bool Hide { get; set; }
    }
}
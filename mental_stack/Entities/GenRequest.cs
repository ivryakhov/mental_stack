namespace MentalStack.Entities
{
    public class GenRequest
    {
        public Meta Meta { get; set; }
        public Request Request { get; set; }
        public Session Session { get; set; }
        public string Version { get; set; }        
    }
}

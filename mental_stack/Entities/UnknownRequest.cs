using MentalStack.Services;

namespace MentalStack.Entities
{
    public class UnknownRequest : IWorkRequest
    {
        public string ProcessRequest(MStackService mStackService)
        {
            return "Неизвестный запрос";
        }
    }
}

using MentalStack.Services;

namespace MentalStack.Entities
{
    public interface IWorkRequest
    {
        string ProcessRequest(MStackService mStackService);
    }
}
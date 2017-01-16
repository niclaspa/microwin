using System.Threading.Tasks;

namespace SampleProject.Services
{
    public interface IHelloService
    {
        string GetGreeting();

        void PostMessage(string message);

        Task PostMessageAsync(string message);
    }
}

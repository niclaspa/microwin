using System.Threading.Tasks;

namespace SampleProject.Owin.Services
{
    public interface IHelloService
    {
        string GetGreeting();

        void PostMessage(string message);

        Task PostMessageAsync(string message);
    }
}

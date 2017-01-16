using System;
using System.Threading.Tasks;

namespace SampleProject.Owin.Services
{
    public class HelloService : IHelloService
    {
        public string GetGreeting()
        {
            return "hello";
        }

        public void PostMessage(string message)
        {
            Console.WriteLine(message);
        }

        public async Task PostMessageAsync(string message)
        {
            Console.WriteLine(message);
        }
    }
}

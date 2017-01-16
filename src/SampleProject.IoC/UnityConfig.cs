using Microsoft.Practices.Unity;
using SampleProject.Services;

namespace SampleProject.IoC
{
    public class UnityConfig
    {
        public static IUnityContainer RegisterGlobalComponents()
        {
            var container = new UnityContainer();

            // lifetime per container
            container.RegisterType<IHelloService, HelloService>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}

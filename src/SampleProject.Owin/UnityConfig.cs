using Microsoft.Practices.Unity;
using SampleProject.Owin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Owin
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

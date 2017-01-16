using Microwin.Hosting.Owin;
using Microwin.Hosting.Unity;
using SampleProject.IoC;
using System;
using System.Web.Http;

namespace SampleProject.Owin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OwinService.Start(GetConfiguration());

            Console.ReadLine();
        }

        public static HttpConfiguration GetConfiguration()
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.RegisterGlobalComponents());

            return config;
        }
    }
}

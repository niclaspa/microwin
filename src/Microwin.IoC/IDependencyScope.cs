using System;
using System.Collections.Generic;

namespace Microwin.IoC
{
    public interface IDependencyScope : IDisposable
    {
        object GetService(Type serviceType);

        IEnumerable<object> GetServices(Type serviceType);
    }
}

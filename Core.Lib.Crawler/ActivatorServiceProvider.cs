using System;

namespace Core.Lib.Crawler
{
    internal class ActivatorServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
            => Activator.CreateInstance(serviceType);
    }
}

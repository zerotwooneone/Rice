using System;
using Microsoft.Extensions.DependencyInjection;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace RiceConsole
{
    public class Startup
    {
        public IServiceProvider GetServiceProvider(IServiceCollection serviceCollection, IUnityContainer unityContainer)
        {
            var serviceProviderFactory = new ServiceProviderFactory(unityContainer);
            var result = serviceProviderFactory.CreateServiceProvider(serviceCollection);
            return result;
        }

        public IUnityContainer GetUnityContainer()
        {
            var uc = new UnityContainer();
            return uc;
        }

        public IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            
            return services;
        }
    }
}
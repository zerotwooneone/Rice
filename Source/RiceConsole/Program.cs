using System;
using Microsoft.Extensions.DependencyInjection;
using Rice.Core;

namespace RiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                var startup = new Startup();
            
                var serviceCollection = startup.ConfigureServices();
                var unityContainer = startup.GetUnityContainer();
                var serviceProvider = startup.GetServiceProvider(serviceCollection, unityContainer);
            
                var y = serviceProvider.GetRequiredService<Class1>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

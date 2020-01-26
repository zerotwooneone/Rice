using System.Reflection;
using System.Runtime.Loader;
using Rice.Core.Abstractions.ModuleLoad;

namespace RiceConsoleClient
{
    class ModuleDependencyLoader : IModuleDependencyLoader
    {
        private readonly AssemblyDependencyResolver _assemblyDependencyResolver;

        public ModuleDependencyLoader(string fullPathToDll)
        {
            _assemblyDependencyResolver = new AssemblyDependencyResolver(fullPathToDll);
        }

        public string ResolveAssemblyToPath(AssemblyName assemblyName) =>
            _assemblyDependencyResolver.ResolveAssemblyToPath(assemblyName);
    }
}
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace PluginApp
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;
        private readonly string _path;
        private readonly string _jobName;

        public PluginLoadContext(string jobName)
            : base(jobName)
        {
            _jobName = jobName;
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _resolver = new AssemblyDependencyResolver(_path);
        }

        public string JobName => _jobName;

        public Assembly PluginBase { get; internal set; }

        public Type PluginType { get; internal set; }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

            if (assemblyPath != null)
                return LoadFromAssemblyPath(assemblyPath);

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

            if (libraryPath != null)
                return LoadUnmanagedDllFromPath(libraryPath);

            return IntPtr.Zero;
        }

        public void LoadPlugins()
        {
            foreach (var pluginAssembly in Directory.GetFiles(_path + "Plugins" + Path.DirectorySeparatorChar + _jobName, "*.dll", SearchOption.AllDirectories))
            {
                LoadFromAssemblyPath(pluginAssembly);
            }

            PluginBase = this.Assemblies
                .Where(PluginHelper.GetPluginType)
                .FirstOrDefault();

            PluginType = this.PluginBase
                .GetTypes()
                .FirstOrDefault(x => x.Name == PluginHelper.PluginInterfaceName);
        }
    }
}

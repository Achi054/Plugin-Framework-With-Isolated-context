using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace PluginApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                IEnumerable<string> plugins = new HashSet<string>
                {
                    "AuditPlugin",
                    "LogPlugin",
                    "SerializationPlugin",
                };

                ISet<PluginLoadContext> contexts = new HashSet<PluginLoadContext>();
                foreach (var plugin in plugins)
                {
                    var pluginContext = new PluginLoadContext(plugin);
                    pluginContext.LoadPlugins();

                    contexts.Add(pluginContext);
                }

                Console.WriteLine("Commands: ");
                foreach (var context in contexts)
                {
                    var pluginExecutors =
                        context.Assemblies.SelectMany(x =>
                                x.GetTypes()
                                .Where(y => context.PluginType.IsAssignableFrom(y) && !y.IsInterface));

                    if (pluginExecutors.Any())
                    {
                        foreach (var executor in pluginExecutors)
                        {
                            MethodInfo methodToExecute = PluginHelper.GetExecutor(context.PluginType);

                            ParameterInfo[] parameterInfo = methodToExecute.GetParameters();

                            var parametersDetails = parameterInfo.Select(x => (x.Name, Version: x.Name == "name" ? context.PluginBase.GetName().Name : context.PluginBase.GetName().Version.ToString()));

                            Object executorInstance = Activator.CreateInstance(executor);

                            if (parametersDetails.Any())
                            {
                                var parameters =
                                    PluginHelper.GetParameterInput(context.PluginBase.GetName().Version.ToString(),
                                        parametersDetails);
                                methodToExecute.Invoke(executorInstance, parameters);
                            }
                            else
                                methodToExecute.Invoke(executorInstance, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                foreach (var assembly in AssemblyLoadContext.Default.Assemblies)
                {
                    Console.WriteLine(assembly.FullName);
                }
            }
        }
    }
}

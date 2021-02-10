using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PluginBase;

namespace SerializationPlugin
{
    public class Executor : ICommand
    {
        public string Name => "Serialization plugin";
        public string Description => "Plugin executor for Serialization";
        public Type AssemblyType => this.GetType();
        public Person Human => new Person
        {
            Age = 10,
            Gender = "Male",
            Name = "Kid Wonder"
        };
        public Task ExecuteAsync(string name, string version)
        {
            var json = JsonConvert.SerializeObject(Human);
            Console.WriteLine($"Executing Serialization Plugin from type {name}.{version}!!");
            Console.WriteLine(json);
            return Task.CompletedTask;
        }

        public string Info() => $"Name: {Name}, Description: {Description}, Type: {AssemblyType.FullName}";
    }
}

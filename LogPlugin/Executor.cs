using System;
using PluginBase;

namespace LogPlugin
{
    public class Executor : ICommand
    {
        public string Name => "Log plugin";
        public string Description => "Plugin executor for Log";
        public int Execute()
        {
            Console.WriteLine("Executing Log Plugin!!");
            return 0;
        }
        public string Info() => $"Name: {Name}, Description: {Description}";
    }
}

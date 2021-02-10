using System;
using PluginBase;

namespace AuditPlugin
{
    public class Executor : ICommand
    {
        public string Name => "Audit plugin";
        public string Description => "Plugin executor for Audit";
        public int Execute()
        {
            Console.WriteLine("Executing Audit Plugin!!");
            return 0;
        }
        public string Info() => $"Name: {Name}, Description: {Description}";
    }
}

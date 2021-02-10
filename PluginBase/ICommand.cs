using System.Threading.Tasks;

namespace PluginBase
{
    public interface ICommand
    {
        string Name { get; }

        string Description { get; }

        Task ExecuteAsync(string name, string version);
    }
}

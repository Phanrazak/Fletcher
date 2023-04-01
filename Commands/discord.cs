using System.Threading.Tasks;

namespace Suzuna_Chan_2.commands
{
    internal class discord
    {
        public static System.Func<object, Task> ComponentInteractionCreated { get; internal set; }
    }
}
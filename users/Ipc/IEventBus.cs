using System.Threading.Tasks;

namespace core_sockets.Ipc
{
    public interface IEventBus
    {
         Task publish(string message, string topic);
    }
}
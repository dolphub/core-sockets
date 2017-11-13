using System.Threading.Tasks;

namespace core_sockets.Ipc
{
    public interface IEventBus
    {
         void publish(string message, string topic);
    }
}
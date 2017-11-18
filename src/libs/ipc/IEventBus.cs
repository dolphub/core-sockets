using System.Threading.Tasks;

namespace Libs.Ipc
{
    public interface IEventBus
    {
        void publish(string message, string topic);
        void registerConsumer();
    }
}
using System;
using System.Threading.Tasks;

namespace core_sockets.Ipc
{
    public class EventBus : IEventBus
    {
        public EventBus()
        {
            //
        }

        public async Task publish(string message, string topic) {
            Console.WriteLine($"Publishing message: {message} on topic: {topic}");
        }
    }
}
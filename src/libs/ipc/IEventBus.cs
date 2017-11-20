using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Libs.Ipc
{
    public interface IEventBus
    {
        void publish(string message, string topic);
        void consumer(string queueName, string routingKey);
    }
}
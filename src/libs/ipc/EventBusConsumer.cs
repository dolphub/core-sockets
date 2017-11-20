using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Libs.Ipc
{
    public class EventBusConsumer
    {
        public EventBusConsumer(string _queueName)
        {
            queueName = _queueName;
        }
        private readonly string queueName;
    }
}
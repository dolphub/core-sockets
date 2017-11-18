using System;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Libs.Ipc
{
    public class EventBus : IEventBus
    {
        public EventBus(IOptions<EventBusConfig> eventBusConfig) {
            config = eventBusConfig.Value;
            Console.WriteLine("Libs.Ipc.EventBus: New - " + config.host);
            var factory = new ConnectionFactory() { 
                HostName = config.host,
                Password = config.password,
                UserName = config.user
            };
            exchange = config.exchange;
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, ExchangeType.Topic);
        }

        private readonly EventBusConfig config;
        private readonly IConnection connection;
        private readonly IModel channel;

        private readonly string exchange;

        public void publish(string message, string routingKey) {
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange, routingKey, false, null, payload);
            Console.WriteLine($"Publishing message: {message} on routingKey: {routingKey}");
        }
    }
}

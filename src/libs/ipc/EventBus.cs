using System;
using RabbitMQ.Client;

namespace Libs.Ipc
{
    public class EventBus : IEventBus
    {
        public EventBus(string host, string user, string password, string exchangeName) {
            Console.WriteLine("Libs.Ipc.EventBus: New - " + host + user + password);
            var factory = new ConnectionFactory() { 
                HostName = host,
                Password = password,
                UserName = user
            };
            exchange = exchangeName;
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, ExchangeType.Topic);
            // https://www.rabbitmq.com/dotnet-api-guide.html
            // @TODO: Close amqp connection
        }

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

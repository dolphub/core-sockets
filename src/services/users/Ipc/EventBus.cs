using System;
using RabbitMQ.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace core_sockets.Ipc
{
    public class EventBus : IEventBus
    {
        public EventBus(IConfiguration configuration) {
            var amqpConfig = configuration.GetSection("EventBusConfig");
            var host = amqpConfig.GetValue("Host", "localhost");
            var user = amqpConfig.GetValue("User", "user");
            var password = amqpConfig.GetValue("Password", "password");
            exchange = amqpConfig.GetValue("Exchange", "hub");
            Console.WriteLine("HOST>>>" + host + user + password);
            var factory = new ConnectionFactory() { 
                HostName = host,
                Password = password,
                UserName = user
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, ExchangeType.Topic);
            // https://www.rabbitmq.com/dotnet-api-guide.html
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
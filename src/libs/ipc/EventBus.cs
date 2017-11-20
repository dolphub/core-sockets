using System;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

        // Register Event Callback: https://stackoverflow.com/questions/2082615/pass-method-as-parameter-using-c-sharp        
        public void consumer(string _queueName, string routingKey) {
            // If no queueName is supplied, generate one to bind key on
            if (string.IsNullOrEmpty(_queueName)) {
                _queueName = Guid.NewGuid().ToString();
            }
            // channel.QueueBind(routingKey, )
            var consumer = new EventingBasicConsumer(channel);
            // @TODO: Implement callbacks
            consumer.Received += (ch, ea) => {
                var body = ea.Body;
                Console.WriteLine("New user created: " + System.Text.Encoding.Default.GetString(body));
            };
            // Binding key to consume on will retrieve messages on routing key published
            channel.QueueDeclare(_queueName, false, false, false, null);
            channel.QueueBind(_queueName,  exchange, routingKey, null);
            String consumerTag = channel.BasicConsume(_queueName, false, consumer);
            
            Console.WriteLine("ConsumerTag: " + consumerTag);
        }
    }
}

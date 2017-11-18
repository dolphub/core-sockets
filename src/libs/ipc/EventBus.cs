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

        // Register Event Callback: https://stackoverflow.com/questions/2082615/pass-method-as-parameter-using-c-sharp
        public void registerConsumer() {
            //
        }
        /**
         * Basic consumer, register delegate for callback on received for this channel
         */
        // var consumer = new EventingBasicConsumer(channel);
        // consumer.Received += (ch, ea) =>
        //         {
        //             var body = ea.Body;
        //             // ... process the message
        //             channel.BasicAck(ea.DeliveryTag, false);
        //         };
        // String consumerTag = channel.BasicConsume(queueName, false, consumer);
    }
}

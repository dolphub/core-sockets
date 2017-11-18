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
using System;
using Libs.Ipc;

namespace Chat.Events
{
    public class NewUserEvent
    {
        public NewUserEvent(IEventBus eventBus)
        {
            Console.WriteLine("NewUserEvent::constructor()");
            eventBus.consumer(_queueName, _routingKey);
        }
        public readonly string _queueName = "";
        public readonly string _routingKey = "user.created";
        
        public Action<string, string[]> registerCallback() {
            return (channel, eventArgs) => {
                Console.WriteLine("GOT RESPONSE>>>> " + eventArgs);
            };
        }
    }
}
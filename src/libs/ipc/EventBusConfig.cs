namespace Libs.Ipc
{
    public class EventBusConfig
    {
        public EventBusConfig() {
            host = "localhost";
            user = "guest";
            password = "password";
            exchange = "default";
        }
        public string host { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string exchange { get; set; }
    }
}
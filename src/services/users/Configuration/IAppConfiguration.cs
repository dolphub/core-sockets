namespace Users.Configuration
{
    public interface IAppConfiguration
    {
         AmqpConfiguration getAmqpConfiguration();

         string getDatabaseConfig();
    }
}
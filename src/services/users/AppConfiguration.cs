using System;
using Microsoft.Extensions.Configuration;

namespace users
{
    public sealed class AppConfiguration
    {
        private readonly AmqpConfiguration amqpConfig;
        private readonly DBConfiguration dbConfig;

        public AppConfiguration(IConfiguration configuration) {
            // Bind AMQP Configuration
            amqpConfig = new AmqpConfiguration(configuration.GetSection("EventBusConfig"));
            dbConfig = new DBConfiguration(configuration.GetConnectionString("DefaultConnection"));
        }

        public AmqpConfiguration getAmqpConfiguration() {
            return amqpConfig;
        }

        public DBConfiguration GetDBConfiguration() {
            return dbConfig;
        }
    }

    public class DBConfiguration {
        protected readonly string dbConnectionString;
        public DBConfiguration(string dbConnection) {
            if (dbConnection.Equals(null)) {
                Console.WriteLine("AppConfiguration.DBConfiguration: No amqp configuration found.");
            }
            dbConnectionString = dbConnection;
        }
    }

    public class AmqpConfiguration
    {
        private readonly string host;
        private readonly string user;
        private readonly string password;
        private readonly string exchange;

        public AmqpConfiguration(IConfigurationSection amqpConfiguration)
        {
            if (amqpConfiguration.Equals(null)) {
                Console.WriteLine("AppConfiguration.AmqpConfiguration: No amqp configuration found.");
            }
            host = amqpConfiguration.GetValue("Host", "localhost");
            user = amqpConfiguration.GetValue("User", "user");
            password = amqpConfiguration.GetValue("Password", "password");
            exchange = amqpConfiguration.GetValue("Exchange", "hub");
        }
    }
}
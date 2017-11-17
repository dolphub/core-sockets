using System;
using Microsoft.Extensions.Configuration;

namespace Users.Configuration
{
    public sealed class AppConfiguration : IAppConfiguration
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

        public string getDatabaseConfig() {
            return dbConfig.connectionString;
        }
    }

    public class DBConfiguration {
        public string connectionString { get; }
        public DBConfiguration(string dbConnection) {
            if (dbConnection.Equals(null)) {
                Console.WriteLine("AppConfiguration.DBConfiguration: No amqp configuration found.");
            }
            connectionString = dbConnection;
        }
    }

    public class AmqpConfiguration
    {
        public readonly string host;
        public readonly string user;
        public readonly string password;
        public readonly string exchange;

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
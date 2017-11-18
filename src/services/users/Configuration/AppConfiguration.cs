using System;
using Microsoft.Extensions.Configuration;

namespace Users.Configuration
{
    public sealed class AppConfiguration
    {
        public AppConfiguration()
        {
        }

        private readonly string dbConnectionString;
    }
}
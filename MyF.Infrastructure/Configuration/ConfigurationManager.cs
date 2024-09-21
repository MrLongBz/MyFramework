using Microsoft.Extensions.Configuration;
using System;

namespace MyF.Infrastructure.Configuration
{
    public static class ConfigurationManager
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static string GetConnectionString(string name)
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("Configuration has not been initialized. Call Initialize method first.");
            }
            return _configuration.GetConnectionString(name);
        }
    }
}

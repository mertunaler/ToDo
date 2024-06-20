using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ToDo.Infrastructure.Configurations;

namespace ToDo.Common
{
    public static class ConfigBuilder
    {
        public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder, 
                        string secretName)
        {
            var configurationSource =new SecretManagerConfigSource(secretName);

            configurationBuilder.Add(configurationSource);
        }
    }
}

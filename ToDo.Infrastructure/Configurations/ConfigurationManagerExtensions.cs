using Microsoft.Extensions.Configuration;

namespace ToDo.Infrastructure.Configurations
{
    public static class ConfigurationManagerExtensions
    {
        public static ConfigurationManager AddSecretsToConfigs(
            this ConfigurationManager manager,
            string secretName)
        {
            IConfigurationBuilder configBuilder = manager;
            configBuilder.Add(new SecretManagerConfigSource(secretName));

            return manager;
        }
    }
}

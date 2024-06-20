using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ToDo.Infrastructure.Configurations
{
    public class SecretManagerConfigProvider : ConfigurationProvider
    {
        private readonly string _region;
        private readonly string _secretName;
        public SecretManagerConfigProvider( string secretName)
        {
            _secretName = secretName;
        }
        public override void Load()
        {
            //string secretData = GetSecrets();
            string secretData = "{\"ApiKey\":\"hello\"}";
            if (!string.IsNullOrEmpty(secretData))
            {
                Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secretData);
            }
        }
        private string GetSecrets()
        {
            try
            {
                var secretsRequest = new GetSecretValueRequest { SecretId = _secretName };
                AmazonSecretsManagerConfig config = new AmazonSecretsManagerConfig()
                {
                    ServiceURL = "http://localhost:4566",
                    AuthenticationRegion = "eu-west-1"
                };

                using (var client = new AmazonSecretsManagerClient(config))
                {
                    var secretsResponse = client.GetSecretValueAsync(secretsRequest).GetAwaiter().GetResult();
                    if (secretsResponse is null)
                        return string.Empty;
                    return secretsResponse.SecretString;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching secrets.", ex);
            }

        }
    }
}

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ShopFood.Domain.Variables;

namespace ShopFood.Domain.Helpers
{
    /// <summary>
    /// Class to Azure Key Vault 
    /// </summary>
    public static class AzureKeyVaultHelper
    {
        /// <summary>
        /// Variable to SecretClient
        /// </summary>
        private static readonly SecretClient _client;

        /// <summary>
        /// Method to set config KeyVaultUri
        /// </summary>
        static AzureKeyVaultHelper()
        {
            var keyVaultUrl = Environment.GetEnvironmentVariable(AppConfig.KeyVaultUri);
            var credential = new DefaultAzureCredential();
            _client = new SecretClient(new Uri(keyVaultUrl), credential);
        }

        /// <summary>
        /// Method to get a value of key vault
        /// </summary>
        /// <param name="secretName">key vault name</param>
        /// <returns>Value</returns>
        public static async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret secret = await _client.GetSecretAsync(secretName);
            return secret.Value;
        }
    }
}

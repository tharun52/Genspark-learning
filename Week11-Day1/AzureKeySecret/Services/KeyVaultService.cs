using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureKeySecret.Interfaces;

namespace AzureKeySecret.Services
{
    public class KeyVaultService : IKeyVaultService
{
    private readonly IConfiguration _configuration;

    public KeyVaultService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GetSecretValueAsync()
    {
        var secretName = _configuration["AzureKeyVault:SecretName"];
        var keyVaultUrl = _configuration["AzureKeyVault:VaultUrl"];

        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        var response = await client.GetSecretAsync(secretName);
        return response.Value.Value; // ✅ actual secret string
    }

}

}
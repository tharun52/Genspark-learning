namespace AzureKeySecret.Interfaces
{
    public interface IKeyVaultService
    {
        Task<string> GetSecretValueAsync();
    }
}
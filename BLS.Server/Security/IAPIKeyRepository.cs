using AspNetCore.Authentication.ApiKey;

namespace BLS.Server.Security
{
    public interface IApiKeyRepository
    {
        Task<IApiKey> GetApiKeyAsync(string key);
    }
}

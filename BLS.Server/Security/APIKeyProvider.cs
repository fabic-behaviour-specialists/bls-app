namespace BLS.Server.Security
{
    using AspNetCore.Authentication.ApiKey;
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly ILogger<IApiKeyProvider> _logger;
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyProvider(ILogger<IApiKeyProvider> logger, IApiKeyRepository apiKeyRepository)
        {
            _logger = logger;
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<IApiKey> ProvideAsync(string key)
        {
            try
            {
                // write your validation implementation here and return an instance of a valid ApiKey or retun null for an invalid key.
                return await _apiKeyRepository.GetApiKeyAsync(key);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}

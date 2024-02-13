namespace BLS.Server.Security
{
    using AspNetCore.Authentication.ApiKey;

    public class APIKeyRepository : IApiKeyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly List<IApiKey> _cache = [];

        public APIKeyRepository(IConfiguration configuration) {
            _configuration = configuration;

            //var apikey = configuration.GetValue<string>("APIKey");
            _cache.Add(new APIKey("Key1", "Key1"));
        }

        public Task<IApiKey> GetApiKeyAsync(string key)
        {
            var apiKey = _cache.FirstOrDefault(k => k.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(apiKey);
        }
    }
}

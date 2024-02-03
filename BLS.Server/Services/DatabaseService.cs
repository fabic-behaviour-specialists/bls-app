using Microsoft.Extensions.Caching.Memory;

namespace BLS.Server.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly IMemoryCache _cache;

        public DatabaseService(ILogger<DatabaseService> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;
        }
    }
}

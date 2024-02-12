using AspNetCore.Authentication.ApiKey;
using System.Security.Claims;

namespace BLS.Server.Security
{
    public class APIKey(string key, string owner, List<Claim>? claims = null) : IApiKey
    {
        public string Key { get; } = key;
        public string OwnerName { get; } = owner;
        public IReadOnlyCollection<Claim> Claims { get; } = claims ?? [];
    }
}

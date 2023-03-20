using Microsoft.Extensions.Caching.Distributed;

namespace API_Ecommerce.Services.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache cache;
        private readonly DistributedCacheEntryOptions options;
        public CachingService(IDistributedCache cache)
        {
            this.cache = cache;
            this.options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
        }

        public async Task<string> GetAsync(string key)
        {
            return await cache.GetStringAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await cache.SetStringAsync(key, value, options);
        }
    }
}

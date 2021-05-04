using System;
using Estudos.App.WebApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Estudos.App.WebApi.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public ResponseCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public void SalvarCache(string key, object value, int tempoDuracaoCache)
        {
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tempoDuracaoCache),
                SlidingExpiration = TimeSpan.FromSeconds((tempoDuracaoCache / 2.0))
            };

            _memoryCache.Set(key, value, memoryCacheEntryOptions);
        }

        public object ObterCache(string key)
        {
            return _memoryCache.TryGetValue(key, out var cacheValue) ? cacheValue : null;
        }

    }
}
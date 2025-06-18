using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task AddAsync<T>(string cacheKey, T value, TimeSpan exprTimeSpan)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow=exprTimeSpan
            };

            memoryCache.Set(cacheKey, cacheOptions);

            return Task.CompletedTask;


            //return memoryCache.GetOrCreateAsync(cacheKey, out T cacheItem);


        }

        public Task<T> GetAsync<T>(string cacheKey)
        {
            if (memoryCache.TryGetValue(cacheKey, out T cacheItem))
            { return  Task.FromResult(cacheItem); }

            return Task.FromResult(default(T));
        }

        public Task RemoveAsync(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
            return Task.CompletedTask;
        }
    }
}

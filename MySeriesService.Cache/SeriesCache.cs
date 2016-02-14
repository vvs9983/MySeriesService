using MySeriesService.Interfaces;
using ServiceStack.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySeriesService.Cache
{
    public class SeriesCache : ISeriesCache
    {
        public string Host => "localhost";

        public Task<long?> GetAsync(string series, int index)
        {
            using (var client = new RedisClient(Host))
            {
                var cache = client.As<Dictionary<int, long>>();

                if (cache.ContainsKey(series) &&
                    cache[series].ContainsKey(index))
                {
                    return Task.FromResult(cache[series][index] as long?);
                }

                return Task.FromResult((long?)null);
            }
        }

        public Task<bool> SetAsync(string series, int index, long value)
        {
            using (var client = new RedisClient(Host))
            {
                var cache = client.As<Dictionary<int, long>>();

                if (cache.ContainsKey(series))
                {
                    cache[series][index] = value;
                }
                else
                {
                    lock (cache)
                    {
                        cache.SetValue(series, 
                            new Dictionary<int, long>()
                        {
                            [index] = value
                        });
                    }
                }

                return Task.FromResult(true);
            }
        }
    }
}

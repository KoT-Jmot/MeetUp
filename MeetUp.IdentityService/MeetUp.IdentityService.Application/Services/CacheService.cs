using System.Text.Json;
using MeetUp.IdentityService.Application.Contracts;
using StackExchange.Redis;

namespace MeetUp.IdentityService.Application.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;
    
    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect("http://redis:6379");
        _cacheDb = redis.GetDatabase();
    }
    
    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);

        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
         
        return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);
    }

    public object RemoveData(string key)
    {
        var exest = _cacheDb.KeyExists(key);

        if (exest)
            return _cacheDb.KeyDelete(key);

        return false;
    }
}
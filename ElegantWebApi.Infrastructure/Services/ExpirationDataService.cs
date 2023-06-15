using ElegantWebApi.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ElegantWebApi.Infrastructure.Services
{
    public sealed class ExpirationDataService : IExprirationDataService
    {
        private ConcurrentDictionary<string, DateTime> _expirationTime;
        private readonly ILogger<IExprirationDataService> _logger;
        public ExpirationDataService(ILogger<IExprirationDataService> logger)
        {
            _expirationTime = new ConcurrentDictionary<string, DateTime>();
            _logger = logger;
        }

        public Task AddExpirationTimeAsync(string key, DateTime expirationTime)
        {
            if (_expirationTime.TryAdd(key, expirationTime))
            {
                _expirationTime[key] = expirationTime;
            };
            _logger.LogInformation($"Added expiration time for record with id: {key}. New expiration time: {expirationTime}");

            return Task.CompletedTask;
        }

        public Task<List<KeyValuePair<string, DateTime>>> GetAllAsync()
        {
            if (_expirationTime.Count <= 0)
            {
                return Task.FromResult(new List<KeyValuePair<string, DateTime>>());
            }

            return Task.FromResult(_expirationTime.ToList());
        }

        public Task<DateTime> GetExprirationTimeAsync(string key)
        {
            if (_expirationTime.TryGetValue(key, out DateTime expirationTime))
            {
                return Task.FromResult(expirationTime);
            }

            return Task.FromResult(DateTime.MinValue);
        }

        public Task UpdateExparationTimeAsync(string key, DateTime expirationTime)
        {
            _expirationTime.AddOrUpdate(key, expirationTime, (k, oldvalue) => expirationTime);
            _logger.LogInformation($"Added expiration time for record with id: {key}. New expiration time: {expirationTime}");

            return Task.CompletedTask;
        }
    }
}

using ElegantWebApi.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ElegantWebApi.Infrastructure.Services
{
    public sealed class ExpirationDataService : IExprirationDataService
    {
        private ConcurrentDictionary<string, DateTime> _expirationTimeTrackingList;
        private readonly ILogger<IExprirationDataService> _logger;
        public ExpirationDataService(ILogger<IExprirationDataService> logger)
        {
            _expirationTimeTrackingList = new ConcurrentDictionary<string, DateTime>();
            _logger = logger;
        }

        public Task AddExpirationTimeAsync(string key, DateTime expirationTime)
        {
            if (_expirationTimeTrackingList.TryAdd(key, expirationTime))
            {
                _expirationTimeTrackingList[key] = expirationTime;
            };
            _logger.LogInformation($"Added expiration time for record with id: {key}. New expiration time: {expirationTime}");

            return Task.CompletedTask;
        }

        public Task<List<KeyValuePair<string, DateTime>>> GetAllAsync()
        {
            if (_expirationTimeTrackingList.Count <= 0)
            {
                return Task.FromResult(new List<KeyValuePair<string, DateTime>>());
            }

            return Task.FromResult(_expirationTimeTrackingList.ToList());
        }

        public Task<DateTime> GetExprirationTimeAsync(string key)
        {
            if (_expirationTimeTrackingList.TryGetValue(key, out DateTime expirationTime))
            {
                return Task.FromResult(expirationTime);
            }

            return Task.FromResult(DateTime.MinValue);
        }

        public Task UpdateExparationTimeAsync(string key, DateTime expirationTime)
        {
            _expirationTimeTrackingList.AddOrUpdate(key, expirationTime, (k, oldvalue) => expirationTime);
            _logger.LogInformation($"Added expiration time for record with id: {key}. New expiration time: {expirationTime}");

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            if (_expirationTimeTrackingList.TryRemove(key, out DateTime expirationTime))
            {
                Task.FromException(new InvalidOperationException());
            }
            return Task.CompletedTask;
        }
    }
}
